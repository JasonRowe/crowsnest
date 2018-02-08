import Vue from 'vue';
import { Component } from 'vue-property-decorator';


@Component
export default class ContainerCountComponent extends Vue {
	count: number = 0;
 
	mounted() {
		fetch('api/Containers/List')
			.then(response => response.json() as Promise<any[]>)
			.then(data => {
				this.count = data.length;
			});
	}
}
