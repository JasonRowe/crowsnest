import Vue from 'vue';
import { Component } from 'vue-property-decorator';

interface PortInfo {
	PrivatePort: number;
	PublicPort: number;
}

interface Container {
	State: string;
	Names: string [];
	Image: number;
	Ports: PortInfo [];
}

@Component
export default class ContainersComponent extends Vue {
	containers: Container[] = [];

	mounted() {
		fetch('api/Containers/List')
			.then(response => response.json() as Promise<Container[]>)
			.then(data => {
				this.containers = data;
			});
	}
}
