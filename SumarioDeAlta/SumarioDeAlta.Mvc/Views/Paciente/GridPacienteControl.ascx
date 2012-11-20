<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<input type="hidden" id="hdPesquisa" />
<div style="margin: 5px" data-role="none">
    <table id="GridPaciente">
    </table>
    <div id="pager">
    </div>
</div>
<script type="text/javascript">
    var _id;
    var _paciente;
    var urlFiltro = '<%= Url.Action("CarregarPacientes", "paciente") %>';

    function FiltrarGrid() {
        urlFiltro = '<%= Url.Action("CarregarPacientes", "paciente") %>/?';

        if ($("#txtPaciente").val() != '')
            urlFiltro += 'nome=' + $("#txtPaciente").val() + '&';

        if ($("#txtProntuario").val() != '')
            urlFiltro += 'prontuario=' + $("#txtProntuario").val() + '&';

        if ($("#txtCpf").val() != '')
            urlFiltro += 'cpf=' + $("#txtCpf").val() + '&';
        
        $("#GridPaciente").setGridParam({ url: urlFiltro });
        $("#GridPaciente").trigger("reloadGrid");
    }

    function AutocompletePaciente(txtPaciente) {
        $("#txtPaciente").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "Paciente/AutoCompletePaciente",
                    data: "{'nome':'" + txtPaciente + "' }",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        response($.map(data, function (item) {
                            return {
                                label: item.name,
                                value: item.id
                            };
                        }));
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert('Ocorreu um erro!');
                    }
                });
            },
            select: function (event, ui) {
                $("#hdPesquisa").attr("value", ui.item.value);
                $("#txtPaciente").attr("value", ui.item.label);
//                jQuery("#GridPaciente").jqGrid('setGridParam', {
//                    url: 'paciente/CarregarPacientes?nome=' + $("#txtPaciente").val()
                //                }).trigger("reloadGrid");
                FiltrarGrid();
                return false;
            },
            focus: function (event, ui) {
                $("#txtPaciente").attr("value", ui.item.label);
                return false;
            },
            minLength: 0
        });
       
    }

    function CarregarGridPaciente() {
        $("#GridPaciente").jqGrid({
            url: urlFiltro,
           // data: { nome: $("#txtPaciente").val() },
            datatype: 'json',
            type: 'POST',
            colNames: ['', 'Id', 'Registro', 'Unidade', 'Paciente', 'Admissao', 'Tipo Paciente', 'Saida'],
            colModel: [
                { name: 'icon', align: 'center', width: 30 },
                { name: 'Id', hidden: true },
                { name: "Registro", index: 'Paciente', width: 155 },
                { name: "Unidade", width: 155 },
                { name: "Paciente", width: 155 },
                { name: "Admissao", width: 155 },
                { name: "TipoPaciente", width: 155 },
                { name: "Saida", width: 155 }
            ],
            height: '100%',
            rowNum: 10,
            rowList: [5, 10, 20, 50],
            viewrecords: true,
            pager: "#pager",
            caption: "Paciente ",
            onSelectRow: function (ids) {
                _paciente = $("#GridPaciente").jqGrid('getCell', ids, 0);
                _id = $("#GridPaciente").jqGrid('getCell', ids, 1);
                $("#img" + ids).click(function () {

                    window.location = '<%= Url.Action("DadosGerais")%>/' + _id;
                });
            },
            gridComplete: function () {
                var ids = $("#GridPaciente").jqGrid('getDataIDs');

                for (var i = 0; i < ids.length; i++) {
                    var graph = "<img src='../../Content/images/1317067945_application_go.png'id=img" + ids[i] + " />";
                    $("#GridPaciente").jqGrid('setRowData', ids[i], { icon: graph });

                }
            }
        });
        $("#GridPaciente").jqGrid('navGrid', '#pager', { add: false, del: false, edit: false, position: 'right', search: false, refresh: false });
    }

    $(function () {

        //AutocompletePaciente($("#txtPaciente").val());

        $("#txtPaciente").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "Paciente/AutoCompletePaciente",
                    data: "{'nome':'" + $("#txtPaciente").val() + "' }",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        response($.map(data, function (item) {
                            return {
                                label: item.name,
                                value: item.id
                            };
                        }));
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert('Ocorreu um erro!');
                    }
                });
            },
            select: function (event, ui) {
                $("#hdPesquisa").attr("value", ui.item.value);
                $("#txtPaciente").attr("value", ui.item.label);
//                                jQuery("#GridPaciente").jqGrid('setGridParam', {
//                                    url: 'paciente/CarregarPacientes?nome=' + $("#txtPaciente").val()
//                                }).trigger("reloadGrid");
                FiltrarGrid();
                return false;
            },
            focus: function (event, ui) {
                $("#txtPaciente").attr("value", ui.item.label);
                return false;
            },
            minLength: 1
        });
        CarregarGridPaciente();
        FiltrarGrid();
        $("#txtPaciente").change(function () {

            if ($("#txtPaciente").val() == "") {

                FiltrarGrid();
            }
        });

    });
    
</script>
