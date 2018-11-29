import Vue from 'vue'
import Router from 'vue-router'
import HelloWorld from '@/components/HelloWorld'
import ObjectEe from '@/components/Object'
import Subject from '@/components/Subject'
import Checking from '@/components/Checking'
import InvestProject from '@/components/InvestProject'
import Holding from '@/components/Holding'
import crud from '@/components/crud'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'HelloWorld',
      component: HelloWorld
    },
    {
      path: '/objects',
      name: 'Object',
      component: ObjectEe
    },
    {
      path: '/subjects',
      name: 'Subject',
      component: Subject
    },
    {
      path: '/checkings',
      name: 'Checking',
      component: Checking
    },
    {
      path: '/projects',
      name: 'InvestProject',
      component: InvestProject
    },
    {
      path: '/holdings',
      name: 'Holding',
      component: Holding
    },
    {
      path: '/crud',
      name: 'crud',
      component: crud
    }
  ]
})
