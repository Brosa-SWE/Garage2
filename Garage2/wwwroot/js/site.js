// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function checkContext() {
	let sortdata = document.querySelector("#sort-data");
	if (sortdata !== null) enableSort(sortdata);
}



function enableSort() {
	sortLp = document.querySelector("#sort-lp");
	sortVt = document.querySelector("#sort-vt");
	sortMk = document.querySelector("#sort-mk");
	sortMd = document.querySelector("#sort-md");
	sortAt = document.querySelector("#sort-at");
	sortPt = document.querySelector("#sort-pt");
	sortLp.addEventListener('mouseup', () => { sortLicensePlate(); });
	sortVt.addEventListener('mouseup', () => { sortVehicleType(); });
	sortMk.addEventListener('mouseup', () => { sortVehicleMake(); });
	sortMd.addEventListener('mouseup', () => { sortVehicleModel(); });
	sortAt.addEventListener('mouseup', () => { sortVehicleCheckIn(); });
	sortPt.addEventListener('mouseup', () => { sortVehicleCheckIn(); });
}

function sortLicensePlate() {
	let list = [];
	let s = function (a, b) {
		return a.childNodes[1].textContent.toLowerCase().localeCompare(b.childNodes[1].textContent.toLowerCase());
	}
	let sortdata = document.querySelector("#sort-data").childNodes;
	for (let i1 = 0; i1 < sortdata.length; i1++) {
		if (sortdata[i1].nodeName === "TR") list.push(sortdata[i1]);
	}
	list.sort(s);
	for (let i1 = 0; i1 < list.length; i1++) { list[i1].parentNode.appendChild(list[i1]); }
}

function sortVehicleType() {
	let list = [];
	let s = function (a, b) {
		return a.childNodes[3].textContent.toLowerCase().localeCompare(b.childNodes[3].textContent.toLowerCase());
	}
	let sortdata = document.querySelector("#sort-data").childNodes;
	for (let i1 = 0; i1 < sortdata.length; i1++) {
		if (sortdata[i1].nodeName === "TR") list.push(sortdata[i1]);
	}
	list.sort(s);
	for (let i1 = 0; i1 < list.length; i1++) { list[i1].parentNode.appendChild(list[i1]); }
}






function sortVehicleMake() {
	let list = [];
	let s = function (a, b) {
		return a.childNodes[5].textContent.toLowerCase().localeCompare(b.childNodes[5].textContent.toLowerCase());
	}
	let sortdata = document.querySelector("#sort-data").childNodes;
	for (let i1 = 0; i1 < sortdata.length; i1++) {
		if (sortdata[i1].nodeName === "TR") list.push(sortdata[i1]);
	}
	list.sort(s);
	for (let i1 = 0; i1 < list.length; i1++) { list[i1].parentNode.appendChild(list[i1]); }
}


function sortVehicleModel() {
	let list = [];
	let s = function (a, b) {
		return a.childNodes[7].textContent.toLowerCase().localeCompare(b.childNodes[7].textContent.toLowerCase());
	}
	let sortdata = document.querySelector("#sort-data").childNodes;
	for (let i1 = 0; i1 < sortdata.length; i1++) {
		if (sortdata[i1].nodeName === "TR") list.push(sortdata[i1]);
	}
	list.sort(s);
	for (let i1 = 0; i1 < list.length; i1++) { list[i1].parentNode.appendChild(list[i1]); }
}


function sortVehicleCheckIn() {
	let list = [];
	let s = function (a, b) {
		return a.childNodes[9].textContent.toLowerCase().localeCompare(b.childNodes[9].textContent.toLowerCase());
	}
	let sortdata = document.querySelector("#sort-data").childNodes;
	for (let i1 = 0; i1 < sortdata.length; i1++) {
		if (sortdata[i1].nodeName === "TR") list.push(sortdata[i1]);
	}
	list.sort(s);
	for (let i1 = 0; i1 < list.length; i1++) { list[i1].parentNode.appendChild(list[i1]); }
}



let sortLp = null; let sortVt = null; let sortMk = null; let sortMd = null; let sortAt = null;let sortPt = null;
checkContext();

