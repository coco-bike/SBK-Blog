$(function () {
    //注册的ajax
    $('#register').click(function () {
        var useremail = $("#exampleInputEmail1").val();
        var userpassword = $("#exampleInputPassword1").val();
        var username1 = $("#exampleInputName").val();
        var username2 = $("#exampleInputLastName").val();
        var usercode = $("#exampleInputCode1").val();
        var username = username1 + username2;
        if (useremail != null && userpassword != null && username1 != null && username2 != null && usercode != null && usercode != null) {
            $.ajax({
                type: "post",
                url: "../../web/user/RegisterUserInfo",
                dataType: "json",
                data: JSON.stringify({ email: useremail, password: userpassword, name: username, code: usercode }),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    if (data.Status == 1) {
                        $(location).attr('href', '../../Web/WebLogin/Index');
                    }
                    if (data.Status != 1) {
                        alert("注册失败,请检查注册的邮箱账号和2次密码是否一致");
                    }
                },
                error: function () {
                    alert("出现未知错误");
                }
            });
        }
    });
    //发送验证码的Ajax
    var InterValObj;
    var count = 60;
    var curCount;
    $("#exampleinputcode").click(function () {
        curCount = count;
        //使button失效，开始计时
        $(this).attr('disabled', 'true');
        $(this).val("请在" + curCount + "秒内输入验证码");
        var useremail = $('#exampleInputEmail1').val();
        InterValObj = window.setInterval(SetRemainTime, 1000);
        //向后台发送数据
        $.ajax({
            type: "post",
            dataType: "JSON",
            url: '../../web/user/GetCode',
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

    $('#exampleConfirmPassword').blur(function () {
        var password1 = $(this).val();
        var password2 = $('#exampleInputPassword1').val();
        if (password1 == password2 && password1 != null && password2 != null) {
            alert('2次密码一致');
        } else {
            alert('2次密码不一致或者等于空');
        }
    });
})