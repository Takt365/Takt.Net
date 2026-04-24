<template>
  <ErrorResult
    status="404"
    code="404"
    title-key="error.notfound.title"
    subtitle-key="error.notfound.subtitle"
    :show-back="true"
    :show-home="true"
  />
</template>

<script setup lang="ts">
import ErrorResult from './components/ErrorResult.vue'
</script>
