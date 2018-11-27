<template>
    <div id="Checking">
        <h1>Выездные проверки инвест проектов</h1>
        <v-data-table
    :headers="headers"
    :items="elements"
    class="elevation-1"
  >
  <template slot="items" slot-scope="props">
      <td>{{ props.item.project }}</td>
      <td>{{ props.item.project }}</td>
      <td>{{ props.item.project }}</td>
      <td>{{ props.item.project }}</td>
      <td>{{ props.item.project }}</td>
    </template>
  </v-data-table>
  </div>
</template>
<script>
export default {
  data () {
    return {
      headers: [
        { text: 'Проект', value: 'project' },
        { text: 'Объект', value: 'project' },
        { text: 'Год начала', value: 'project' },
        { text: 'Год окончания', value: 'project' },
        { text: 'Тип', value: 'project' }
      ],
      elements: [
        {project: 'test'}
      ]
    }
  }
}
</script>
<style>
#Checking {
}
.grid{
    margin: 0% auto
    }
</style>
