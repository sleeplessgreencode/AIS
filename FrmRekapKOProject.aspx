<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmRekapKOProject.aspx.vb" Inherits="AIS.FrmRekapKOProject" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
#chartdiv {
	width		: 100%;
	height		: 500px;
	font-size	: 9px;
}						
</style>

<!-- Resources -->
<script type="text/javascript" src="js/amcharts/amcharts.js"></script>
<script type="text/javascript" src="js/amcharts/serial.js"></script>
<script type="text/javascript" src="js/amcharts/plugins/export/export.min.js"></script>
<link rel="stylesheet" href="js/amcharts/plugins/export/export.css" type="text/css" media="all" />
<script type="text/javascript" src="js/amcharts/themes/light.js"></script>
<!-- Chart code -->
<script type="text/javascript">
    $(function () {
        $.ajax({
            type: "POST",
            url: "FrmRekapKOProject.aspx/GetData",
            data: '{}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccess,
            failure: function (response) {
                alert(response.d);
            },
            error: function (response) {
                alert(response.d);
            }
        });
    });

    function OnSuccess(response) {
        var chart = AmCharts.makeChart("chartdiv", {
            "theme": "light",
            "type": "serial",
            "dataProvider": response.d,
            "valueAxes": [{
                "stackType": "3d",
                "unit": "",
                "position": "left",
                "title": "Nilai dalam jutaan Rp.",
            }],
            "startDuration": 1,
            "graphs": [{
                "balloonText": "Realisasi: <b>[[value]]</b>",
                "fillAlphas": 0.9,
                "lineAlpha": 0.2,
                "title": "",
                "type": "column",
                "valueField": "TotalKO"
            }, {
                "balloonText": "Rencana: <b>[[value]]</b>",
                "fillAlphas": 0.9,
                "lineAlpha": 0.2,
                "title": "",
                "type": "column",
                "valueField": "TotalRAP"
            }],
            "plotAreaFillAlphas": 0.1,
            "depth3D": 60,
            "angle": 30,
            "categoryField": "JobNo",
            "categoryAxis": {
                "gridPosition": "start"
            },
            "export": {
    	        "enabled": true
             }
        });
        jQuery('.chart-input').off().on('input change',function() {
	        var property	= jQuery(this).data('property');
	        var target		= chart;
	        chart.startDuration = 0;

	        if ( property == 'topRadius') {
		        target = chart.graphs[0];
      	        if ( this.value == 0 ) {
                  this.value = undefined;
      	        }
	        }

	        target[property] = this.value;
	        chart.validateNow();
        });
    };
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div style="font-family:Segoe UI Light">
<table>
<tr>
    <td style="font-size:30px; text-decoration:underline">Rekap KO (Semua Job)</td>
</tr>
</table>
</div>

<div class="font1">
<table>
<tr>
    <td>
        <dx:ASPxButton ID="BtnPrint" runat="server" Text="PRINT" 
            Theme="MetropolisBlue" Width="75px" TabIndex="1">
        </dx:ASPxButton>  
    </td>
</tr>
</table>

<div id="chartdiv"></div>	
</div>

</asp:Content>
