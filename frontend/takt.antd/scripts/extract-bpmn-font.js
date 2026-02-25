const fs = require('fs');
const path = require('path');
const src = path.join(__dirname, '..', 'node_modules', 'bpmn-js', 'dist', 'assets', 'bpmn-font', 'css', 'bpmn-embedded.css');
const raw = fs.readFileSync(src, 'utf8');
const lines = raw.split(/\r?\n/);
const out = [];
let i = 0;
while (i < lines.length) {
  const line = lines[i];
  if (line.startsWith('@font-face')) {
    const block = [line];
    i++;
    while (i < lines.length && !lines[i].trim().startsWith('}')) block.push(lines[i++]);
    block.push(lines[i++]);
    const blockStr = block.join('\n');
    if (blockStr.includes("url('data:application/octet-stream;base64,")) {
      out.push(blockStr);
    }
  } else if (line.indexOf('[class^="bpmn-icon-"]') >= 0) {
    while (i < lines.length) out.push(lines[i++]);
    break;
  }
  i++;
}
const dest = path.join(__dirname, '..', 'src', 'assets', 'styles');
if (!fs.existsSync(dest)) fs.mkdirSync(dest, { recursive: true });
fs.writeFileSync(path.join(dest, 'bpmn-font-embedded-only.css'), out.join('\n'), 'utf8');
console.log('Wrote bpmn-font-embedded-only.css');
