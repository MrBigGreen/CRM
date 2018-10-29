/**
 * @license Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.html or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	 
config.toolbarGroups = [
     
      { name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
       { name: 'colors' },
      '/',
      { name: 'styles' },
{ name: 'basicstyles', groups: [ 'basicstyles', 'cleanup' ] },
		{ name: 'paragraph',   groups: [ 'list', 'indent', 'blocks', 'align' ] }
      
    ];
config.height = 200;
config.width = 690;
    // 取消“拖拽以改变尺寸”功能 plugins / resize / plugin.js
    // config.resize_enabled = false;
config.entities_greek = true; // 是否转换一些难以显示的字符为相应的 HTML字符 
//config.fontSize_defaultLabel = '12px';
config.language = 'zh-cn';
    //工具栏默认是否展开
config.toolbarStartupExpanded = true;
    // config.toolbarCanCollapse = true;
    // Remove some buttons, provided by the standard plugins, which we don't
    // need to have in the Standard(s) toolbar.
config.removeButtons = 'Underline,Subscript,Superscript';
config.enterMode = CKEDITOR.ENTER_BR;
config.shiftEnterMode = CKEDITOR.ENTER_P;
	
};
