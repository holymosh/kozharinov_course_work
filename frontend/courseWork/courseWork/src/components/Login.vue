<template>
    <div class="login">
        <v-text-field v-model="user.username" label="Логин"></v-text-field>
        <v-text-field v-model="user.password" label="Пароль"></v-text-field>
        <v-btn color="primary" dark class="mb-2" to="/objects" @click="login">Войти</v-btn>
    </div>
</template>

<script>

export default {
  data: () => ({
    user: {
      username: '',
      password: ''
    }
  }),
  created () {
    this.initialize()
  },
  methods: {
    initialize () {
    },
    login () {
      var el = document.getElementById('menu')
      el.style.display = 'inline'
    }
  }
}
</script>

<style scoped>
.login{
margin: 15% 35%
}
</style>
