<template>
    <div id="TestComponent">
        <h1>Открыт тестовый компонент</h1>
        <h2>{{hMessage}}</h2>
    </div>
</template>

<script>
export default {
  name: 'TestComponent',
  data () {
    return {
      hMessage: 'это тестовый компонент'
    }
  }
}
</script>
<style>
</style>
