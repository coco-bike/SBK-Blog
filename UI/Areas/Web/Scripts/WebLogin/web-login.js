jQuery.support.cors = true;
$(function () {
    $('#login').click(function () {
        var useremail = $("#exampleInputEmail1").val();
        var userpassword = $("#exampleInputPassword1").val();
        var autologinText = $("input[type='checkbox']").is(':checked');
        $.ajax({
            type: "post",
            url: "../../web/Weblogin/CheckLoginInfo",
            dataType: "json",
            data: JSON.stringify({ email: useremail, password: userpassword, autologin: autologinText }),
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.Status == 1) {
                   $(location).attr('href','../../Web/Home/Index');
                }
                if (data.Status != 1) {
                    alert("登录失败,请检查账户名和密码");
                }
            },
            error: function () {
                alert("出现未知错误");
            }
        });
    })
})
