function doSearch() {
    $('#dg1').datagrid('load', {
        username: $('#username').val()
    });
};

function reset() {
    $('#username').val("");
    $('#dg1').datagrid('load', {
        username: $('#username').val()
    });
}

function newUser() {
    $('#dlg').dialog('open').dialog('setTitle', 'New User');
    $('#fm').form('clear');
    url = '../../Admin/AdminUser/SaveAdminUserData';
}

function editUser() {
    var row = $('#dg1').datagrid('getSelected');
    if (row) {
        $('#dlg').dialog('open').dialog('setTitle', 'Edit User');
        $('#fm').form('load', row);
        url = "../../Admin/AdminUser/UpdateAdminUserData";
    }
}

function bindRole() {
    var nametext = $("#Name").val();
    var typetext = $("#Type").val();
    var teltext = $("#TelNumber").val();
    var rolestext = $("#Roles").val();
    url="../../Admin/AdminUser/AddAdminRole"
    $.ajax({
        type: "Post",
        url: url,
        data: {
            'Name': nametext,
            'Type': typetext,
            'TelNumber': teltext,
            'Roles': rolestext
        },
        dataType: "Json",
        success: function (data) {
            alert("ok");
        }
    });
}

function saveUser() {
    var row = $('#dg1').datagrid('getSelected');
    if (row == null) {
        idtext = null;
    } else {
        var idtext = row.Id;
    }
    var nametext = $("#Name").val();
    var typetext = $("#Type").val();
    var teltext = $("#TelNumber").val();
    var rolestext = $("#Roles").val();
    $.ajax({
        type: "post",
        url: url,
        data: {
            'id': idtext,
            'Name': nametext,
            'Type': typetext,
            'TelNumber': teltext,
            'Roles': rolestext
        },
        dataType: "Json",
        success: function (data) {
            if (url = '../../Admin/AdminUser/SaveAdminUserData') {
                bindRole();
            }
            $('#dlg').dialog('close'); // close the dialog
            $('#dg1').datagrid('reload');
        },
        error: function () {
            alert("出现错误");
        }
    });
}


function saveUser1() {
    $('#fm').form('submit', {
        url: url,
        success: function (result) {
            var result = eval('(' + result + ')');
            if (result.errorMsg) {
                $.messager.show({
                    title: 'Error',
                    msg: result.errorMsg
                });
            } else {
                $('#dlg').dialog('close'); // close the dialog
                $('#dg1').datagrid('reload'); // reload the user data
            }
        }
    });
}

function destroyUser() {
    var row = $('#dg1').datagrid('getSelected');
    if (row) {
        $.messager.confirm('Confirm', 'Are you sure you want to destroy this user?', function (r) {
            if (r) {
                $.post('../../Admin/AdminUser/DeleteAdminUserData', {
                    id: row.Id
                }, function (result) {
                    if (result == 1) {
                        $('#dg1').datagrid('reload'); // reload the user data
                    } else {
                        $.messager.show({ // show error message
                            title: 'Error',
                            msg: result.errorMsg
                        });
                    }
                }, 'json');
            }
        });
    }
}