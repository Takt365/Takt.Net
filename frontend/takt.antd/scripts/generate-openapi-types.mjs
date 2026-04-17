import { mkdir } from 'node:fs/promises'

import http from 'node:http'

import https from 'node:https'

import { dirname, resolve } from 'node:path'

import process from 'node:process'

import { fileURLToPath } from 'node:url'

import openapiTS, { astToString } from 'openapi-typescript'



const __filename = fileURLToPath(import.meta.url)

const __dirname = dirname(__filename)

const projectRoot = resolve(__dirname, '..')



/**

 * 与后端 TaktSwaggerCollectionExtensions 中文档名一致（OpenAPI：`/openapi/{documentName}.json`）

 */

const SWAGGER_DOCUMENT_NAMES = [

  'Accounting',

  'Generator',

  'HumanResource',

  'Identity',

  'Logistics',

  'Routine',

  'Statistics',

  'Workflow'

]



const defaultOpenapiBase = 'https://localhost:60071'

const openapiBase = (process.env.TAKT_OPENAPI_BASE || defaultOpenapiBase).replace(/\/$/, '')

const allowInsecureTls = process.env.TAKT_OPENAPI_INSECURE === '1'

/** 逗号分隔，仅生成指定分组，例如 Identity,Generator */

const documentsFilter = process.env.TAKT_OPENAPI_DOCUMENTS?.trim()

const outputDir = resolve(projectRoot, 'src/types/generated')



const SELF_SIGNED_CERT_CODES = new Set(['DEPTH_ZERO_SELF_SIGNED_CERT', 'SELF_SIGNED_CERT_IN_CHAIN'])



function documentSlug(name) {

  return name.toLowerCase()

}



function openapiUrlForDocument(documentName) {

  return `${openapiBase}/openapi/${documentName}.json`

}



function getErrorCodes(error) {

  const codes = new Set()

  if (error?.code) codes.add(error.code)

  if (error?.originalError?.code) codes.add(error.originalError.code)

  if (error?.originalError?.cause?.code) codes.add(error.originalError.cause.code)

  if (error?.cause?.code) codes.add(error.cause.code)

  return codes

}



function isSelfSignedCertError(error) {

  const codes = getErrorCodes(error)

  for (const code of codes) {

    if (SELF_SIGNED_CERT_CODES.has(code)) {

      return true

    }

  }

  return false

}



/** 仅本次请求跳过证书校验 */

function fetchUrlInsecureOnce(urlString) {

  const u = new URL(urlString)

  const lib = u.protocol === 'https:' ? https : http

  const requestOptions =

    u.protocol === 'https:' ? { rejectUnauthorized: false } : {}



  return new Promise((resolve, reject) => {

    lib

      .get(urlString, requestOptions, (res) => {

        if (res.statusCode && res.statusCode >= 400) {

          reject(new Error(`HTTP ${res.statusCode} ${res.statusMessage || ''}`.trim()))

          res.resume()

          return

        }

        const chunks = []

        res.on('data', (chunk) => chunks.push(chunk))

        res.on('end', () => resolve(Buffer.concat(chunks).toString('utf8')))

        res.on('error', reject)

      })

      .on('error', reject)

  })

}



async function buildOpenapiAstForUrl(openapiUrl) {

  try {

    return await openapiTS(new URL(openapiUrl))

  } catch (error) {

    const needInsecure =

      allowInsecureTls || (!allowInsecureTls && isSelfSignedCertError(error))

    if (needInsecure) {

      const u = new URL(openapiUrl)

      if (u.protocol !== 'https:' && u.protocol !== 'http:') {

        throw error

      }

      if (allowInsecureTls) {

        console.warn(`[contracts] insecure tls fetch (TAKT_OPENAPI_INSECURE=1): ${openapiUrl}`)

      } else {

        console.warn(

          `[contracts] detected self-signed certificate, retrying without cert verify: ${openapiUrl}`

        )

      }

      const body = await fetchUrlInsecureOnce(openapiUrl)

      return await openapiTS(body)

    }

    throw error

  }

}



function resolveDocumentList() {

  if (documentsFilter) {

    const wanted = new Set(

      documentsFilter

        .split(',')

        .map((s) => s.trim())

        .filter(Boolean)

    )

    const list = SWAGGER_DOCUMENT_NAMES.filter((n) => wanted.has(n))

    const unknown = [...wanted].filter((n) => !SWAGGER_DOCUMENT_NAMES.includes(n))

    if (unknown.length) {

      console.warn('[contracts] unknown document names (ignored):', unknown.join(', '))

    }

    if (list.length === 0) {

      throw new Error(

        `[contracts] TAKT_OPENAPI_DOCUMENTS 未匹配任何已知分组。可选: ${SWAGGER_DOCUMENT_NAMES.join(', ')}`

      )

    }

    return list

  }

  return [...SWAGGER_DOCUMENT_NAMES]

}



function barrelExportLine(documentName) {

  const slug = documentSlug(documentName)

  const pascal = documentName

  return [

    `export type {`,

    `  paths as ${pascal}Paths,`,

    `  webhooks as ${pascal}Webhooks,`,

    `  components as ${pascal}Components,`,

    `  $defs as ${pascal}Defs,`,

    `  operations as ${pascal}Operations`,

    `} from './openapi-${slug}'`

  ].join('\n')

}



async function writeBarrelFile(documentNames) {
  const fs = await import('node:fs/promises')
  const header = `/**\n * 本文件由 scripts/generate-openapi-types.mjs 生成，请勿手改。\n * 按后端 OpenAPI 文档分组，各分组见 openapi-*.d.ts。\n */\n\n`
  const lines = documentNames.map((n) => `${barrelExportLine(n)}\n`).join('\n')
  await fs.writeFile(resolve(outputDir, 'index.d.ts'), header + lines, 'utf8')
  try {
    await fs.unlink(resolve(outputDir, 'openapi.d.ts'))
  } catch {
    /* 不存在则忽略 */
  }
}



async function generateContracts() {

  const documents = resolveDocumentList()

  console.log(`[contracts] base: ${openapiBase}`)

  console.log(`[contracts] documents: ${documents.join(', ')}`)

  console.log(`[contracts] output dir: ${outputDir}`)



  await mkdir(outputDir, { recursive: true })

  const fs = await import('node:fs/promises')



  for (const doc of documents) {

    const url = openapiUrlForDocument(doc)

    const slug = documentSlug(doc)

    const filePath = resolve(outputDir, `openapi-${slug}.d.ts`)

    console.log(`[contracts] ${doc} <- ${url}`)

    const ast = await buildOpenapiAstForUrl(url)

    const generated = astToString(ast)

    await fs.writeFile(filePath, generated, 'utf8')

    console.log(`[contracts] wrote ${filePath}`)

  }



  await writeBarrelFile(documents)

  /** 与后端 Swagger 文档名对齐后，移除旧分组文件名，避免重复导出与陈旧契约。 */
  const legacyGenerated = ['openapi-organization.d.ts', 'openapi-logging.d.ts']
  for (const name of legacyGenerated) {
    try {
      await fs.unlink(resolve(outputDir, name))
      console.log(`[contracts] removed legacy ${name}`)
    } catch {
      /* 不存在则忽略 */
    }
  }

  console.log('[contracts] wrote index.d.ts')

  console.log('[contracts] done')

}



function printOpenapiUrlHint() {

  console.error('[contracts] hint: 各分组 URL 形如：')

  for (const name of SWAGGER_DOCUMENT_NAMES) {

    console.error(`  ${openapiBase}/openapi/${name}.json`)

  }

  console.error('[contracts] hint: TAKT_OPENAPI_BASE 覆盖主机；TAKT_OPENAPI_DOCUMENTS=Identity,Generator 仅生成子集')

}



generateContracts().catch((error) => {

  console.error('[contracts] failed')

  if (isSelfSignedCertError(error)) {

    console.error('[contracts] hint: trust local dev cert or run with TAKT_OPENAPI_INSECURE=1')

  }

  const msg = String(error?.message ?? error?.originalError?.message ?? '')

  if (msg.includes('404')) {

    printOpenapiUrlHint()

  }

  console.error(error)

  process.exit(1)

})

