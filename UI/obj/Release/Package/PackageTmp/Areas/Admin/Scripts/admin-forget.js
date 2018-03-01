$(function () {
    restpic();
});

var namestr;
function submitForm() {
    $('#ff').form('submit', {
        url: '../../Admin/AdminLogin/ValidatePwdBackCode',
        onSubmit: function () {
            return $(this).form('validate');
        },
        success: function (data) {
            var dataobject = $.parseJSON(data);
            alert(dataobject.Msg);
            $('#dd').css('display', 'block');
            $('#ff').css('display', 'none');
            $('#ck-submit-2').css('display', 'block');
            $('#ck-submit-1').css('display', 'none');
            namestr = $('#name').val();
        }
    });
};
function submit() {
    var pwd = $('#password').val();
    var pwd1 = $('#passwordCheck').val();
    if (pwd == pwd1) {
        $.ajax({
            type: "post",
            url: "../../Admin/AdminLogin/UpdateAdminUserData",
            data: { 'name': namestr, 'password': pwd },
            dataType: "Json",
            success: function (data) {
                alert(data.Msg);
                $(location).attr('href', '../../Admin/Admin/Index');
            }
        });
    } else {
        alert("2次输入的密码不一致");
    }

}
//发送验证码的Ajax
var InterValObj;
var count = 60;
var curCount;
$("#exampleinputcode").click(function () {
    curCount = count;
    //使button失效，开始计时
    $(this).attr('disabled', 'true');
    $(this).val("请在" + curCount + "秒内输入验证码");
    var useremail = $('#eamil').val();
    InterValObj = window.setInterval(SetRemainTime, 1000);
    //向后台发送数据
    $.ajax({
        type: "post",
        dataType: "JSON",
        url: '../../Admin/UserAdmin/CheckUserData',
        data: JSON.stringify({ email: useremail }),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            alert(data.Msg);
        },
        error: function (data) {
            alert(data.Msg);
        }
    });
});

function SetRemainTime() {
    if (curCount == 0) {
        window.clearInterval(InterValObj);
        $('#exampleinputcodes').removeAttr("disabled");
        $('#exampleinputcode').val("重新发送验证码");
    } else {
        curCount--;
        $("#exampleinputcode").val("请在" + curCount + "秒内输入验证码");
    }
}

function restpic() {
    $('#ck-imgcode1').attr('src', '/Admin/AdminLogin/GetForgetPasswordVerifiedCode?time=' + Math.random());
}

