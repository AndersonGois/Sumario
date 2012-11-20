/* Usado para minimizar a configuração do jqGrid nas páginas */
$(document).ready(function() {
    $.jgrid.defaults = $.extend($.jgrid.defaults, {
        datatype: "json",
        mtype: "POST",
        height: '100%',
        jsonReader: {
            repeatitems: false,
            root: "Items",
            records: "ItemCount",
            page: "PageIndex",
            total: "TotalPages"
        },
        viewrecords: true
    });
})