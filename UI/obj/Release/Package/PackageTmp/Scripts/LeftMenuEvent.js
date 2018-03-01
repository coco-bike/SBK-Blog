//创建Frame
function createFramePage(url) {
    var s = '<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>';
    return s;
}
//添加Tab
function addTab1(url, icon, titletext) {
    if (!$('#tabs').tabs('exists', titletext)) {
        $('#tabs').tabs('add', {
            title: titletext,
            content: createFramePage(url),
            closable: true,
            icon: '',
        });
    } else {
        $('#tabs').tabs('select', titletext);
    }
    tabClose();
}
//初始化按钮操作
$(function () {
    $('#_easyui_tree_2').click(function () {
        addTab1('../../Admin/AdminUser/Index', '', '后台人员管理');
    });
    $('#_easyui_tree_4').click(function () {
        addTab1('../../Admin/WebUser/Index', '', '前台人员管理');
    });
    $('#_easyui_tree_6').click(function () {
        addTab1('../../Admin/AdminRole/Index', '', '后台角色管理');
    });
    $('#_easyui_tree_8').click(function () {
        addTab1('../../Admin/WebUser/Index', '', '前台角色管理');
    });
    $('#_easyui_tree_10').click(function () {
        addTab1('../../Admin/AdminAuthority/Index', '', '后台权限管理');
    });
    $('#_easyui_tree_12').click(function () {
        addTab1('../../Admin/WebAuthority/Index', '', '前台权限管理');
    });
});