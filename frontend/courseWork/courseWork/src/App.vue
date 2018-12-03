<template>
  <div id="app" >
    <div class="menu">
    <v-toolbar dark dense>
      <v-toolbar-side-icon to="/">
      </v-toolbar-side-icon>
      <v-toolbar-items>
        <v-btn flat to="/objects">Объекты</v-btn>
        <v-btn flat to="/subjects">Субъекты</v-btn>
        <v-btn flat to="/projects">Проекты</v-btn>
        <v-btn flat to="/checkings">Проверки</v-btn>
        <v-btn flar to="/holdings">Холдинги</v-btn>
        <v-btn flar to="/equipment">Оборудование</v-btn>
      </v-toolbar-items>
    </v-toolbar>
    </div>
    <div class="view">
    <router-view/>
    </div>
  </div>
</template>

<script>
export default {
  name: 'App'
}
</script>

<style scoped>
a:visited {
  text-decoration: none;
  color: white;
}
a:link {
  text-decoration: none;
}
.view{
  margin:0 2%;
}
</style>
