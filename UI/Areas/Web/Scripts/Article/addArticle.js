$(function () {
    //初始化
    var ue = UE.getEditor('container');

    //绑定发布点击事件
    $("#submit").click(function () {
        posthtml();
    });

    //绑定取消点击事件
    $("#cancel").click(function () {
        cancelhtml();
    });

    //获取分类
    getclassifica();
});

function getclassifica() {
    $.ajax({
        type: "Get",
        url: "url",
        data: "",
        dataType: "Json",
        success: function (data) {
            count = data.length;
            for (var i = 0; i < count; i++) {
                var str;
                str = "<label><input name='Fruit' type='radio' value=" + JSON.stringify(data[i].valueid) + " " + "/>" + data[i].name + "</label>"
                $(".article-classification").append(str);
            };
        },
        error: function () {
            alert("检查网络");
        }
    });
}


function cancelhtml() {
    var ue = UE.getEditor('container');
    ue.setContent() = "";
    ue.clearLocalData();
}

function posthtml() {
    var ue = UE.getEditor('container');
    var content = ue.getContent();
    var titletext = $("#input-title").val();
    var typeid = $("input:radio[name='Fruit']:checked").val();
    var checkbox1 = $("#checkbox-1").is(":checked");
    var checkbox2 = $("#checkbox-2").is(":checked");
    $.ajax({
        type: "post",
        url: "../../Article/PostArticle",
        data: {
            'htmltext': content,
            'title': titletext,
            'typeid': typeid,
            'checkbox1': checkbox1,
            'checkbox2': checkbox2
        },
        dataType: "Json",
        success: function (data) {
            alert(data.Msg);
        }
    });
}
