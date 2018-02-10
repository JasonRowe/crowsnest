import Vue from 'vue';
import { Component } from 'vue-property-decorator';

interface PortInfo {
	PrivatePort: number;
	PublicPort: number;
}

interface Container {
	id: string;
	state: string;
	names: string [];
	image: number;
	ports: PortInfo[];
	ipAddresses: string[];
}

// @ts-ignore
@Component
export default class ContainersComponent extends Vue {
	containers: Container[] = [];
	checkedContainers: Container[] = [];
	showAll: boolean = false;

	mounted() {
		this.getContainersList();
	}

	getContainersList(){
		fetch(`api/Containers/List/?showAll=${this.showAll}`)
			.then(response => response.json() as Promise<Container[]>)
			.then(data => {
				this.containers = data;
			});
	}

	start() {
		this.checkedContainers.forEach(container => {
			fetch(`api/Containers/Start/?id=${container.id}`)
			.then(response => response.json() as Promise<boolean>)
			.then(result => {
				if(result){
					this.getContainersList();
				}
			});
		});
	}

	stop() {
		this.checkedContainers.forEach(container => {
			fetch(`api/Containers/Stop/?id=${container.id}`)
			.then(response => response.json() as Promise<boolean>)
			.then(result => {
				if(result){
					this.getContainersList();
				}
			});
		});
	}

	kill() {
		this.checkedContainers.forEach(container => {
			fetch(`api/Containers/Kill/?id=${container.id}`)
		});
	}
}
