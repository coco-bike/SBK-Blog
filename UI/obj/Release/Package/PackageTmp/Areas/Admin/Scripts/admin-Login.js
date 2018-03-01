$(function () {
    restimg();
});
//自定义验证规则
$.extend($.fn.validatebox.defaults.rules, {
    Password: {
        validator: function (value) {
            var autoby = /^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{6,10}$/.test($.trim(value));
            return autoby
        },
        message: '密码要求必须包含数字、字母，6-10位'
    },
    Length: {
        validator: function (value) {
            return value.length == 4;
        },
        message: '请输入4个字符'
    },
    UserStr: {
        validator: function (value) {
            return (value.length >= 3&& value.length <= 8);
        },
        message:'请输入字数大于3个，小于8个'
    }
    
});
//提交表单数据
function submitForm() {
    //$("#ff").form('submit', {
    //    url: "../../Admin/Login/CheckLoginInfo",
    //    onSubmit: ,
    //    success: function (data) {
    //        var dataobject= $.parseJSON(data);
    //        alert(dataobject.Msg);
    //    }
    //});
    var checkform = function () {
        var val = $(this).form('validate')
        return val;
    };
    if (checkform) {
        var nametext = $('#name').val();
        var pwdtext = $('#pwd').val();
        var codetext = $('#verifyCode').val();
        var autoLogintext = $('#autoLogin').is(':checked');
        $.ajax({
            url: "../../Admin/AdminLogin/CheckLoginInfo",
            type: "Post",
            data: {
                username: nametext,
                pwd: pwdtext,
                verifyCode: codetext,
                autoLogin: autoLogintext
            },
            dataType: "Json",
            success: function (data) {
                if(data.Status==1){
                    $(location).attr('href', '../../Admin/Admin/Index');
                }
                else{
                    alert("请求出错");
                }
            },
            error: function (data) {
                alert(data.Msg);
            }
        });
    }

};
//重新获取验证码
function restimg() {
    $('#ck-imgcode').attr('src', '/Admin/AdminLogin/GetVerifiedCode?time=' + Math.random());
}