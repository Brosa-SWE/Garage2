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
	sortLp.addEventListener('mouseup', () => { sortColumn(1); });
	sortVt.addEventListener('mouseup', () => { sortColumn(3); });
	sortMk.addEventListener('mouseup', () => { sortColumn(5); });
	sortMd.addEventListener('mouseup', () => { sortColumn(7); });
	sortAt.addEventListener('mouseup', () => { sortColumn(9); });
	sortPt.addEventListener('mouseup', () => { sortColumnReverse(9); });
}


function sortColumn(column) {
	let list = [];
	let s = function (a, b) {
		return a.childNodes[column].textContent.toLowerCase().localeCompare(b.childNodes[column].textContent.toLowerCase());
	}
	let sortdata = document.querySelector("#sort-data").childNodes;
	for (let i1 = 0; i1 < sortdata.length; i1++) {
		if (sortdata[i1].nodeName === "TR") list.push(sortdata[i1]);
	}
	list.sort(s);
	for (let i1 = 0; i1 < list.length; i1++) { list[i1].parentNode.appendChild(list[i1]); }
}

function sortColumnReverse(column) {
	let list = [];
	let s = function (a, b) {
		return b.childNodes[column].textContent.toLowerCase().localeCompare(a.childNodes[column].textContent.toLowerCase());
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

