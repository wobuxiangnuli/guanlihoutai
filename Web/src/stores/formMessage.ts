import { defineStore } from 'pinia';

export const useFormMessage = defineStore('formMessage',{
    state:()=> ({
        pageid:'',
        pageMessage:[],
        formList:[],
        rules:[] as any
    }),
    actions:{
        async setPageMessage (data:any){
            this.pageMessage = data
        },
        async setFormList (data:any){
            this.formList = data
        },
        async setRules (data:any){
            this.rules.push(data)
        }
    }
}) 