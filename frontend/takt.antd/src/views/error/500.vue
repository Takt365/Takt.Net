<template>
  <ErrorResult
    status="500"
    code="500"
    title-key="error.servererror.title"
    subtitle-key="error.servererror.subtitle"
    :show-back="true"
    :show-home="true"
    :show-reload="true"
  />
</template>

<script setup lang="ts">
import ErrorResult from './components/ErrorResult.vue'
</script>
