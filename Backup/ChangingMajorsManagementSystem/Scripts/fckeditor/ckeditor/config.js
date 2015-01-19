/*
Copyright (c) 2003-2011, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function(config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';

    // 界面语言,默认为'en'
    config.language = 'zh-cn';

    // 设置宽高
    config.width = 755;
    config.height = 185;

    // 编辑器样式:  'kama'(默认).'office2003'.'v2'
    //config.skin = 'v2';

    // 背景颜色
    //config.uiColor = '#FFF';

    // 工具栏配置
    config.toolbar = 'Full';
    config.toolbar_Full = [
        ['Source', '-', 'NewPage', 'Preview', '-', 'Templates'],
        ['Undo', 'Redo', '-', 'Find', 'Replace', '-', 'SelectAll', 'RemoveFormat'],
        ['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'],
        ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote'],
        ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
        ['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak'],
        ['Styles', 'Format', 'Font', 'FontSize'],
        ['TextColor', 'BGColor'],
        ['Maximize', 'ShowBlocks', '-', 'About']
    ];
    config.toolbarCanCollapse = true;

    // 上传路径的配置
    //config.filebrowserBrowserUrl = '/FckEditors/UploadFiles';
    //config.filebrowserImageBrowserUrl = '/FckEditors/UploadFiles';
    //config.filebrowserWindowWidth = '800';
    //config.filebrowserWindowHeight = '500';
    // 工具栏默认是否展开
    //config.toolbarStartupExpanded = false;
    // 当提交有此编辑器的表单时,是否自动更新元素的数据
    config.autoUpdateElement = true;
};