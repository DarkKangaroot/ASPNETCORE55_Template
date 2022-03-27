<template>
    <div>
        <v-dialog
            v-model="confirmDialogConfig.dialog"
            :max-width="confirmDialogConfig.dialogOptions.width"
            persistent
            @keydown.enter="agreeConfirmDialog"
        >
            <v-card>
            <v-toolbar dark :color="confirmDialogConfig.dialogOptions.color" dense flat>
                <v-toolbar-title class="text-body-2 font-weight-bold" v-text="confirmDialogConfig.title"></v-toolbar-title>
            </v-toolbar>
            <v-card-text
                v-show="!!confirmDialogConfig.message"
                class="pa-4 black--text"
                v-html="confirmDialogConfig.message"
            ></v-card-text>
            <v-card-actions class="pt-3">
                <v-spacer></v-spacer>
                <v-btn
                v-if="!confirmDialogConfig.dialogOptions.noconfirm"
                text
                class="body-2 font-weight-bold"
                @click.native="cancelConfirmDialog"
                >Cancel</v-btn
                >
                <v-btn
                color="primary"
                class="body-2 font-weight-bold"
                text
                @click.native="agreeConfirmDialog"
                >OK</v-btn
                >
            </v-card-actions>
            </v-card>
        </v-dialog>
    </div>
</template>

<script lang="ts">
import { Mixins, Component  } from 'vue-property-decorator'
import Mixin from "@/plugins/mixins.vue"

@Component({ })
export default class ConfirmDialogComponent extends Mixins(Mixin) {
}
</script>