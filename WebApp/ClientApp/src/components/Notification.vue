<template>
    <div>
        <v-snackbar
            v-model="config.isNotificationShow"
            :timeout="5000"
            right
            :color="config.color"
            rounded="0"
        >
            {{ config.message }}
            </v-snackbar>
    </div>
</template>

<script lang="ts">
import { Vue, Component,  } from 'vue-property-decorator'
import { namespace } from 'vuex-class';
import { INotification } from '@/interface/INotification' 

const notificationStore = namespace('NotificationStore');

@Component({})
export default class ActivityTableCreateComponent extends Vue {

    @notificationStore.State
    config!: INotification
}

</script>