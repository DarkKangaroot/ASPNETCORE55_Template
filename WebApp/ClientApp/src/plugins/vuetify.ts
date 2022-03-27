import Vue from 'vue';
import Vuetify from 'vuetify/lib/framework';

Vue.use(Vuetify);

export default new Vuetify({
});


export type VForm = Vue & {
    validate: () => boolean;
    resetValidation: () => boolean;
    resetFields: () => void;
}

export type VDialog = Vue & {
    save: (value: unknown) => void;
}