﻿var hdstylemenu =
    '<style>#hdstylemenu a,#hdstylemenu a:focus,#hdstylemenu a:hover {color: inherit !important;text-decoration: none;}</style><div id="absolutestyleeditor" style="position: fixed;bottom: 0px;width: 100%;" ><div class="hdpanel-group" id="accordion" role="tablist" aria-multiselectable="true" style="margin-bottom: 0px;"><div class="hdpanel hdpanel-success"><div class="hdpanel-heading" style="padding: 4px;"><h6 class="hdpanel-title">   <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseStyle" style="font-size: 12px !important;" aria-expanded="true"  aria-controls="collapseStyle">Style Editor</a></h6></div> <div id="collapseStyle" class="hdpanel-collapse collapse in" role="tabhdpanel" aria-labelledby="headingOne"><div class="hdpanel-body" ><div class="hdpanel-group" id="hdstylemenu" role="tablist" aria-multiselectable="false"></div></div></div></div></div></div>';
var hd_rightmenu =
    '<style>#hd_rightmenu .selectize-control.multi .selectize-input.input-active:after,#hd_rightmenu .selectize-control.multi .selectize-input:after,#hd_rightmenu .selectize-control.single .selectize-input.input-active:after,#hd_rightmenu .selectize-control.single .selectize-input:after{display:none} .hd_borderselected{ 1px dashed rgba(0, 0, 0, 0.22);} #hd_rightmenu .selectize-input{border-radius: 0px !important;} #hd_rightmenu .selectize-dropdown .active {background-color: #252424 !important; color: #ffffff;} #hd_rightmenu .selectize-dropdown .option  { padding: 3px 6px !important;} #hd_rightmenu .selectize-control.hdform-control .item{ line-height:20px; margin: 0 1px 1px 0 !important;background-color: #0f03f4;padding: 0px 3px 0px 5px;color: #FFFFFF;}  #hd_rightmenu .selectize-control.hdform-control{ height:auto;} #hd_rightmenu{ font-size:11px !important; }#hd_rightmenu .selectize-control.single .item{ background-color: #ffffff;padding: 1px;color: #656464;} #hd_rightmenu .selectize-control .selectize-input.has-items, #hd_rightmenu input { font-size:11px !important;padding: 0px 0px 0px;} #hd_rightmenu .hdform-group{ margin-top:0px; }#hd_rightmenu .toggle{ margin-left: 10px; }</style><div class="" ' + 'style="' + 'position:absolute;top:40px;left:20px;width:180px;z-index:10000;font-size:12px !important;" ' + 'id="hd_rightmenu">' + '<div class="hdcol-xs-50">' + '<div class="hdpanel hdpanel-info hdcol-xs-50" style="background-color: rgba(255, 255, 255, 0.83);padding: 0px" >' + '<div class="hdpanel-heading hdcol-xs-50" id="hd_rightmenu_header">' + '<h3 class=" hdpanel-title">hdpanel info</h3>' + '</div>' + '<div class="hdpanel-body open hdcol-xs-50" id="hd_rightmenu_body" style="">' + '<div class="hdcol-xs-50" style="">' + '<div ><div id="hd_rightmenu_allattributes" class="hd_rightmenu_clear  hdcol-xs-50 "></div></div>' + '<div class="hidden"><a href="javascript:void(0)">Style</a><div id="hd_rightmenu_styles" class="hd_rightmenu_clear  hdcol-xs-50 "></div></div>' + '</div>' + '</div>' + ' </div>' + ' </div>' + ' </div><datalist id="hd_stylelist"></datalist>';
var newattributehtml = '      <div class="hdcol-xs-50 ">  <div class="hdform-group hdform-group-sm">'
    + '          <label class="control-label pull-left">'
    + 'New Attribute'
    + '</label>'
    + '<input type="text" class="hdform-control input-sm " placeholder="Key" id="hd_rightmenu_attr_name" />'
    + '<input type="text" class="hdform-control input-sm " placeholder="Value" id="hd_rightmenu_attr_value"/>'
    + '          '
    + '       </div> </div>';