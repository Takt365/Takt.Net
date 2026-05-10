<template>
  <ErrorResult
    status="warning"
    code="503"
    title-key="error.serviceunavailable.title"
    subtitle-key="error.serviceunavailable.subtitle"
    :show-back="true"
    :show-home="true"
  />
</template>

<script setup lang="ts">
import ErrorResult from './components/ErrorResult.vue'
</script>
