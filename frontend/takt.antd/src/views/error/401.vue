<template>
  <ErrorResult
    status="warning"
    code="401"
    title-key="error.unauthorized.title"
    subtitle-key="error.unauthorized.subtitle"
    :show-back="true"
    :show-home="true"
    :show-login-primary="true"
  />
</template>

<script setup lang="ts">
import ErrorResult from './components/ErrorResult.vue'
</script>
