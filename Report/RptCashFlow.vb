﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports System
Imports System.ComponentModel


Public Class RptCashFlow
    Inherits ReportClass
    
    Public Sub New()
        MyBase.New
    End Sub
    
    Public Overrides Property ResourceName() As String
        Get
            Return "RptCashFlow.rpt"
        End Get
        Set
            'Do nothing
        End Set
    End Property
    
    Public Overrides Property NewGenerator() As Boolean
        Get
            Return true
        End Get
        Set
            'Do nothing
        End Set
    End Property
    
    Public Overrides Property FullResourceName() As String
        Get
            Return "AIS.RptCashFlow.rpt"
        End Get
        Set
            'Do nothing
        End Set
    End Property
    
    <Browsable(false),  _
     DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
    Public ReadOnly Property Section1() As CrystalDecisions.CrystalReports.Engine.Section
        Get
            Return Me.ReportDefinition.Sections(0)
        End Get
    End Property
    
    <Browsable(false),  _
     DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
    Public ReadOnly Property Section2() As CrystalDecisions.CrystalReports.Engine.Section
        Get
            Return Me.ReportDefinition.Sections(1)
        End Get
    End Property
    
    <Browsable(false),  _
     DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
    Public ReadOnly Property Section3() As CrystalDecisions.CrystalReports.Engine.Section
        Get
            Return Me.ReportDefinition.Sections(2)
        End Get
    End Property
    
    <Browsable(false),  _
     DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
    Public ReadOnly Property Section4() As CrystalDecisions.CrystalReports.Engine.Section
        Get
            Return Me.ReportDefinition.Sections(3)
        End Get
    End Property
    
    <Browsable(false),  _
     DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
    Public ReadOnly Property Section5() As CrystalDecisions.CrystalReports.Engine.Section
        Get
            Return Me.ReportDefinition.Sections(4)
        End Get
    End Property
    
    <Browsable(false),  _
     DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
    Public ReadOnly Property Parameter_PrintInfo() As CrystalDecisions.[Shared].IParameterField
        Get
            Return Me.DataDefinition.ParameterFields(0)
        End Get
    End Property
    
    <Browsable(false),  _
     DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
    Public ReadOnly Property Parameter_Periode() As CrystalDecisions.[Shared].IParameterField
        Get
            Return Me.DataDefinition.ParameterFields(1)
        End Get
    End Property
    
    <Browsable(false),  _
     DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
    Public ReadOnly Property Parameter_JobNm() As CrystalDecisions.[Shared].IParameterField
        Get
            Return Me.DataDefinition.ParameterFields(2)
        End Get
    End Property
    
    <Browsable(false),  _
     DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
    Public ReadOnly Property Parameter_NettKontrak() As CrystalDecisions.[Shared].IParameterField
        Get
            Return Me.DataDefinition.ParameterFields(3)
        End Get
    End Property
    
    <Browsable(false),  _
     DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
    Public ReadOnly Property Parameter_CadanganKSO() As CrystalDecisions.[Shared].IParameterField
        Get
            Return Me.DataDefinition.ParameterFields(4)
        End Get
    End Property
    
    <Browsable(false),  _
     DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
    Public ReadOnly Property Parameter_Alokasi_A() As CrystalDecisions.[Shared].IParameterField
        Get
            Return Me.DataDefinition.ParameterFields(5)
        End Get
    End Property
    
    <Browsable(false),  _
     DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
    Public ReadOnly Property Parameter_Alokasi_B() As CrystalDecisions.[Shared].IParameterField
        Get
            Return Me.DataDefinition.ParameterFields(6)
        End Get
    End Property
    
    <Browsable(false),  _
     DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
    Public ReadOnly Property Parameter_Alokasi_C() As CrystalDecisions.[Shared].IParameterField
        Get
            Return Me.DataDefinition.ParameterFields(7)
        End Get
    End Property
    
    <Browsable(false),  _
     DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
    Public ReadOnly Property Parameter_Alokasi_F() As CrystalDecisions.[Shared].IParameterField
        Get
            Return Me.DataDefinition.ParameterFields(8)
        End Get
    End Property
    
    <Browsable(false),  _
     DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
    Public ReadOnly Property Parameter_Alokasi_G() As CrystalDecisions.[Shared].IParameterField
        Get
            Return Me.DataDefinition.ParameterFields(9)
        End Get
    End Property
    
    <Browsable(false),  _
     DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
    Public ReadOnly Property Parameter_Alokasi_H() As CrystalDecisions.[Shared].IParameterField
        Get
            Return Me.DataDefinition.ParameterFields(10)
        End Get
    End Property
    
    <Browsable(false),  _
     DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
    Public ReadOnly Property Parameter_Alokasi_I() As CrystalDecisions.[Shared].IParameterField
        Get
            Return Me.DataDefinition.ParameterFields(11)
        End Get
    End Property
    
    <Browsable(false),  _
     DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
    Public ReadOnly Property Parameter_Alokasi_T() As CrystalDecisions.[Shared].IParameterField
        Get
            Return Me.DataDefinition.ParameterFields(12)
        End Get
    End Property
    
    <Browsable(false),  _
     DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
    Public ReadOnly Property Parameter_HutangProc() As CrystalDecisions.[Shared].IParameterField
        Get
            Return Me.DataDefinition.ParameterFields(13)
        End Get
    End Property
    
    <Browsable(false),  _
     DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
    Public ReadOnly Property Parameter_Alokasi_D() As CrystalDecisions.[Shared].IParameterField
        Get
            Return Me.DataDefinition.ParameterFields(14)
        End Get
    End Property
    
    <Browsable(false),  _
     DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
    Public ReadOnly Property Parameter_Alokasi_E() As CrystalDecisions.[Shared].IParameterField
        Get
            Return Me.DataDefinition.ParameterFields(15)
        End Get
    End Property
    
    <Browsable(false),  _
     DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
    Public ReadOnly Property Parameter_Alokasi_L() As CrystalDecisions.[Shared].IParameterField
        Get
            Return Me.DataDefinition.ParameterFields(16)
        End Get
    End Property
    
    <Browsable(false),  _
     DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
    Public ReadOnly Property Parameter_TerminShare() As CrystalDecisions.[Shared].IParameterField
        Get
            Return Me.DataDefinition.ParameterFields(17)
        End Get
    End Property
End Class

<System.Drawing.ToolboxBitmapAttribute(GetType(CrystalDecisions.[Shared].ExportOptions), "report.bmp")>  _
Public Class CachedRptCashFlow
    Inherits Component
    Implements ICachedReport
    
    Public Sub New()
        MyBase.New
    End Sub
    
    <Browsable(false),  _
     DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
    Public Overridable Property IsCacheable() As Boolean Implements CrystalDecisions.ReportSource.ICachedReport.IsCacheable
        Get
            Return true
        End Get
        Set
            '
        End Set
    End Property
    
    <Browsable(false),  _
     DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
    Public Overridable Property ShareDBLogonInfo() As Boolean Implements CrystalDecisions.ReportSource.ICachedReport.ShareDBLogonInfo
        Get
            Return false
        End Get
        Set
            '
        End Set
    End Property
    
    <Browsable(false),  _
     DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
    Public Overridable Property CacheTimeOut() As System.TimeSpan Implements CrystalDecisions.ReportSource.ICachedReport.CacheTimeOut
        Get
            Return CachedReportConstants.DEFAULT_TIMEOUT
        End Get
        Set
            '
        End Set
    End Property
    
    Public Overridable Function CreateReport() As CrystalDecisions.CrystalReports.Engine.ReportDocument Implements CrystalDecisions.ReportSource.ICachedReport.CreateReport
        Dim rpt As RptCashFlow = New RptCashFlow()
        rpt.Site = Me.Site
        Return rpt
    End Function
    
    Public Overridable Function GetCustomizedCacheKey(ByVal request As RequestContext) As String Implements CrystalDecisions.ReportSource.ICachedReport.GetCustomizedCacheKey
        Dim key As [String] = Nothing
        '// The following is the code used to generate the default
        '// cache key for caching report jobs in the ASP.NET Cache.
        '// Feel free to modify this code to suit your needs.
        '// Returning key == null causes the default cache key to
        '// be generated.
        '
        'key = RequestContext.BuildCompleteCacheKey(
        '    request,
        '    null,       // sReportFilename
        '    this.GetType(),
        '    this.ShareDBLogonInfo );
        Return key
    End Function
End Class
