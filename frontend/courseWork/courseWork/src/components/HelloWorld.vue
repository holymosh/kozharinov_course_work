<template>
  <div class="hello" id="HelloWorld">
    <h1>Курсовая работа</h1>
    <h2>Меню:</h2>
    <ul>
      <li>Объекты - объекты электроэнергетики</li>
      <li>Субъекты - субъекты электроэнергетики </li>
      <li>Проекты - инвестиционные проекты, которые относятся к объектам</li>
      <li>Провеки - выездные проверки инвестиционных проектов </li>
    </ul>
  </div>
</template>

<script>
export default {
  name: 'HelloWorld',
  data () {
    return {
      msg: 'Welcome to Your Vue.js App'
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
#HelloWorld{
}
h1{
  text-align: center;
  font-family: Arial, Helvetica, sans-serif;
}
h2{
  padding-bottom: 5px;
  padding-left: 1%;
}
li{
  font-size: 12pt
}
</style>
