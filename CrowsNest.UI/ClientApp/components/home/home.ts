import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component({
    components: {
        CountComponent: require('../containerCount/containerCount.vue.html')
    }
})
export default class homeComponent extends Vue {
}