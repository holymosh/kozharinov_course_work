<template>
    <div class="login">
        <v-text-field v-model="user.username" label="Логин"></v-text-field>
        <v-text-field v-model="user.password" label="Пароль"></v-text-field>
        <v-btn color="primary" dark class="mb-2" @click="login">Войти</v-btn>
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
      var username = this.user.username
      if (username === 'admin' || username === 'read' || username === 'readwrite') {
        window.role = username
        this.$router.push('/objects')
        var el = document.getElementById('menu')
        el.style.display = 'inline'
      }
      else{
        alert('Пользователь не найден')
      }
    }
  }
}
</script>

<style scoped>
.login{
margin: 15% 35%
}
</style>
