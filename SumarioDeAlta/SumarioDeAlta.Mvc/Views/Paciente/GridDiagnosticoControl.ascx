<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div style="margin: 5px" data-role="none">
    <table id="GridDiagnostico">
    </table>
    <div id="pager">
    </div>
</div>
<script type="text/javascript">
    $(function () {

        $("#GridDiagnostico").jqGrid({
            url: '../CarregarDiagnostico/',
            data: {},
            datatype: 'json',
            type: 'POST',
            colNames: ['Tipo Diagnóstico', 'Código CID', 'CID'],
            colModel: [
                { name: "TipoDiagnostico", width: 300 },
                { name: "CodigoCid", width: 155 },
                { name: "Cid", width: 700 }

            ],
            height: '100%',
            rowNum: 10,
            rowList: [5, 10, 20, 50],
            viewrecords: true,
            pager: "#pager"
        });
        $("#GridDiagnostico").jqGrid('navGrid', '#pager', { add: false, del: false, edit: false, position: 'right', search: false, refresh: false });

    });
  
   
</script>