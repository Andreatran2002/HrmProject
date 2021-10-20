$(document).ready(function(){

/* ==========================================================================
	Tables
	========================================================================== */

	var $table = $('#table'),
		$remove = $('#remove'),
		selections = [];

	function totalTextFormatter(data) {
		return 'Total';
	}

	function totalModeFormatter(data) {
		return data.length;
	}

	function totalEventFormatter(data) {
		var total = 0;
		$.each(data, function (i, row) {
			total += +(row.event.substring(1));
		});
		return total;
	}
	function totalRetFormatter(data) {
		var total = 0;
		$.each(data, function (i, row) {
			total += +(row.ret.substring(1));
		});
		return total;
	}
	function totalSupFormatter(data) {
		var total = 0;
		$.each(data, function (i, row) {
			total += +(row.sup.substring(1));
		});
		return total;
	}
	function totalSetFormatter(data) {
		var total = 0;
		$.each(data, function (i, row) {
			total += +(row.set.substring(1));
		});
		return total;
	}
	function statusFormatter(data, rowData, index) {
		var classBtn = '',
			classDropup = '',
			pageSize = 10;

		if (data === 'Serious ') classBtn = 'btn-danger';
		if (data === 'GOOD') classBtn = 'btn-primary';
		if (data === 'Issue') classBtn = 'btn-warning';
		if (data === 'FINE') classBtn = 'btn-success';

		if (index >= pageSize / 2) {
			classDropup = 'dropup';
		}

		return	'<div class="dropdown dropdown-status ' +
				classDropup +
				' ">' +
				'<button class="btn ' +
				classBtn +
				' " type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">' +
				data;
	}

	window.operateEvents = {
		'click .like': function (e, value, row, index) {
			alert('You click like action, row: ' + JSON.stringify(row));
		},
		'click .remove': function (e, value, row, index) {
			$table.bootstrapTable('remove', {
				field: 'update',
				values: [row.id]
			});
		}
	};

	function operateFormatter(value, row, index) {
		return [
			'<a class="like" href="javascript:void(0)" title="Like">',
			'<i class="glyphicon glyphicon-heart"></i>',
			'</a>  ',
			'<a class="remove" href="javascript:void(0)" title="Remove">',
			'<i class="glyphicon glyphicon-remove"></i>',
			'</a>'
		].join('');
	}

	function getIdSelections() {
		return $.map($table.bootstrapTable('getSelections'), function (row) {
			return row.id
		});
	}

	var data = [
		{
			"update": "19/05 10:17:30",
			"mode": "Serious ",
			"event": "Nghiêm trọng: 6N.18C.1",
			"ret": "----",
			"sup": "----",
			"set": "----"
		},
		{
			"update": "19/05 10:32:30",
			"mode": "GOOD",
			"event": "----",
			"ret": "-13,1",
			"sup": "-14,8",
			"set": "-14,5"
		},
		{
			"update": "19/05 10:47:30",
			"mode": "Issue",
			"event": "Bị ngắt kết nối",
			"ret": "----",
			"sup": "----",
			"set": "----"
		},
		{
			"update": "19/05 11:02:30",
			"mode": "FINE",
			"event": "----",
			"ret": "-13,1",
			"sup": "-18,8",
			"set": "-11,5"
			
		},
		{
			"update": "19/05 11:17:30",
			"mode": "GOOD",
			"event": "----",
			"ret": "-13,1",
			"sup": "-14,8",
			"set": "-14,5"
		},
		{
			"update": "19/05 11:32:30",
			"mode": "GOOD",
			"event": "----",
			"ret": "-13,1",
			"sup": "-14,8",
			"set": "-14,5"
		},
		{
			"update": "19/05 11:47:30",
			"mode": "GOOD",
			"event": "----",
			"ret": "-13,1",
			"sup": "-14,8",
			"set": "-14,5"
		},
		{
			"update": "19/05 12:02:30",
			"mode": "GOOD",
			"event": "----",
			"ret": "-13,1",
			"sup": "-14,8",
			"set": "-14,5"
		},
		{
			"update": "19/05 12:17:30",
			"mode": "GOOD",
			"event": "----",
			"ret": "-13,1",
			"sup": "-14,8",
			"set": "-14,5"
		},
		{
			"update": "19/05 12:32:30",
			"mode": "GOOD",
			"event": "----",
			"ret": "-13,1",
			"sup": "-14,8",
			"set": "-14,5"
		},
		{
			"update": "19/05 12:47:30",
			"mode": "GOOD",
			"event": "----",
			"ret": "-13,1",
			"sup": "-14,8",
			"set": "-14,5"
		},
		{
			"update": "19/05 13:02:30",
			"mode": "GOOD",
			"event": "----",
			"ret": "-13,1",
			"sup": "-14,8",
			"set": "-14,5"
		},
		{
			"update": "19/05 13:17:30",
			"mode": "GOOD",
			"event": "----",
			"ret": "-13,1",
			"sup": "-14,8",
			"set": "-14,5"
		},
		{
			"update": "19/05 13:32:30",
			"mode": "GOOD",
			"event": "----",
			"ret": "-13,1",
			"sup": "-14,8",
			"set": "-14,5"
		},
		{
			"update": "19/05 13:47:30",
			"mode": "GOOD",
			"event": "----",
			"ret": "-13,1",
			"sup": "-14,8",
			"set": "-14,5"
		},
		{
			"update": "19/05 14:02:30",
			"mode": "GOOD",
			"event": "----",
			"ret": "-13,1",
			"sup": "-14,8",
			"set": "-14,5"
		},
		{
			"update": "19/05 14:17:30",
			"mode": "GOOD",
			"event": "----",
			"ret": "-13,1",
			"sup": "-14,8",
			"set": "-14,5"
		},
		{
			"update": "19/05 14:32:30",
			"mode": "GOOD",
			"event": "----",
			"ret": "-13,1",
			"sup": "-14,8",
			"set": "-14,5"
		},
		{
			"update": "19/05 14:47:30",
			"mode": "GOOD",
			"event": "----",
			"ret": "-13,1",
			"sup": "-14,8",
			"set": "-14,5"
		},
		{
			"update": "19/05 15:02:30",
			"mode": "GOOD",
			"event": "----",
			"ret": "-13,1",
			"sup": "-14,8",
			"set": "-14,5"
		},
		{
			"update": "19/05 15:17:30",
			"mode": "GOOD",
			"event": "----",
			"ret": "-13,1",
			"sup": "-14,8",
			"set": "-14,5"
		},
		{
			"update": "19/05 15:32:30",
			"mode": "GOOD",
			"event": "----",
			"ret": "-13,1",
			"sup": "-14,8",
			"set": "-14,5"
		}
	];
	
	$table.bootstrapTable({
		iconsPrefix: 'font-icon',
		icons: {
			paginationSwitchDown:'font-icon-arrow-square-down',
			paginationSwitchUp: 'font-icon-arrow-square-down up',
			refresh: 'font-icon-refresh',
			toggle: 'font-icon-list-square',
			columns: 'font-icon-list-rotate',
			export: 'font-icon-download',
			detailOpen: 'font-icon-edit',
			Close: 'font-icon-minus-1'
		},
		paginationPreText: '<i class="font-icon font-icon-arrow-left"></i>',
		paginationNextText: '<i class="font-icon font-icon-arrow-right"></i>',
		data: data,
		columns: [
			[
				{
					title: 'Update time',
					field: 'update',
					rowspan: 2,
					align: 'center',
					valign: 'middle',
					sortable: true,
					footerFormatter: totalTextFormatter
				},
				{
					title: 'Từ ngày 12/05/2020 đến 19/09/2020',
					colspan: 5,
					align: 'center'
				}
			],
			[
				{
					field: 'mode',
					title: 'Mode',
					sortable: true,
					editable: true,
					formatter: statusFormatter,
					footerFormatter: totalModeFormatter,
					align: 'center'
				},
				{
					field: 'event',
					title: 'Event',
					sortable: true,
					align: 'center',
					footerFormatter: totalEventFormatter
				},
				{
					field: 'ret',
					title: 'Ret',
					align: 'center',
					sortable: true,
					footerFormatter: totalRetFormatter
				},
				{
					field: 'sup',
					title: 'Sup',
					align: 'center',
					sortable: true,
					footerFormatter: totalSupFormatter
				},
				{
					field: 'set',
					title: 'Set',
					align: 'center',
					sortable: true,	
					footerFormatter: totalSetFormatter
				}
			]
		]
	});

	$table.on('check.bs.table uncheck.bs.table ' +
		'check-all.bs.table uncheck-all.bs.table', function () {
		$remove.prop('disabled', !$table.bootstrapTable('getSelections').length);
		// save your data, here just save the current page
		selections = getIdSelections();
		// push or splice the selections if you want to save all data selections
	});

	$remove.click(function () {
		var ids = getIdSelections();
		$table.bootstrapTable('remove', {
			field: 'update',
			values: ids
		});
		$remove.prop('disabled', true);
	});

	$('#toolbar').find('select').change(function () {
		$table.bootstrapTable('refreshOptions', {
			exportDataType: $(this).val()
		});
	});

/* ========================================================================== */
});
