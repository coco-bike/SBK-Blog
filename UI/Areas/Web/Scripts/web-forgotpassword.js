$(function () {

    var email = null;
    $('#reset').click(function () {
        var useremail = $("#exampleInputEmail1").val();
        $.ajax({
            type: "post",
            url: "../../web/user/FindUserInfo",
            dataType: "json",
            data: JSON.stringify({ email: useremail }),
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                if (data.Status == 1) {
                    $('#reset').addClass('yt-hide');
                    $('#group1').addClass('yt-hide');
                    $('#group2').removeClass('yt-hide');
                    $('#code').removeClass('yt-hide');
                    email = useremail;
                }
                if (data.Status != 1) {
                    alert("请检查你的邮箱是否正确");
                }
            },
            error: function () {
                alert("出现未知错误");
            }
        });
    });
    $('#code').click(function () {
        var usercode = $('#exampleInputCode').val();
        if (usercode != null) {
            $.ajax({
                type: "post",
                url: "../../web/user/ValidatePwdBackCode",
                dataType: "json",
                data: JSON.stringify({ code: usercode }),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    $('#group2').addClass('yt-hide');
                    $('#code').addClass('yt-hide');
                    $('#group3').removeClass('yt-hide');
                    $('#group4').removeClass('yt-hide');
                    $('#enter').removeClass('yt-hide');
                }
            });
        }
    });

    $('#enter').click(function () {
        var password1 = $('#exampleInputPassword1').val();
        var password2 = $('#exampleInputPassword2').val();

        if (password1 != null && password2 != null && password1 == password2) {
            $.ajax({
                type: "post",
                url: "../../web/user/GetPassword",
                dataType: "json",
                data: JSON.stringify({ email: email, password: password1 }),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    alert('修改完成');
                    $(location).attr('href', '../../Web/WebLogin/Index');
                }
            });
        }
    })
});