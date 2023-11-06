import { defineStore } from 'pinia';

export const useFormMessage = defineStore('formMessage',{
    state:()=> ({
        pageid:'',
        pageMessage:[]
    }),
    actions:{
        async setPageMessage (data:any){
            this.pageMessage = data
        }
    }
}) 