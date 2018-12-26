<template>
  <div id="app" >
    <div id="menu" class="menu">
    <v-toolbar dark dense>
      <v-toolbar-side-icon to="/">
      </v-toolbar-side-icon>
      <v-toolbar-items>
        <v-btn flat to="/objects">Объекты</v-btn>
        <v-btn flat to="/subjects">Субъекты</v-btn>
        <v-btn flat to="/projects">Проекты</v-btn>
        <v-btn flat to="/checkings">Проверки</v-btn>
        <v-btn flat to="/holdings">Холдинги</v-btn>
        <v-btn flat to="/equipment">Оборудование</v-btn>
        <v-spacer></v-spacer>
        <v-btn flat to="/login" @click="logout">Выйти</v-btn>
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
  name: 'App',
  methods: {
    logout () {
      var el = document.getElementById('menu')
      el.style.display = 'none'
    }
  }
}
</script>

<style>
a:visited {
  text-decoration: none;
  color: white;
}
.hidden{
  display: none;
}
a:link {
  text-decoration: none;
}
.view{
  margin:0 2%;
}
</style>
