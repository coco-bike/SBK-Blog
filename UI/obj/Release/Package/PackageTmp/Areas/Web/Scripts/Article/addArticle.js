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

function cancelhtml() {
    var ue = UE.getEditor('container');
    ue.setContent() = "";
    ue.clearLocalData();
}

function posthtml() {
    var ue = UE.getEditor('container');
    var content = ue.getContent();
    var summary = ue.getContentTxt().substring(0,100);
    var titletext = $("#input-title").val();
    var typeid = $("input:radio[name='Fruit']:checked").val();
    var checkbox1 = $("#checkbox-1").is(":checked");
    if(content!=null&&titletext!=null&&typeid!=null&&checkbox1!=null){
        $.ajax({
            type: "post",
            url: "../../Web/Article/AddArticleContent",
            data: {
                'htmltext': content,
                'title': titletext,
                'typeid': typeid,
                'summary':summary,
                'checkbox1': checkbox1,
            },
            dataType: "Json",
            success: function (data) {
                alert(data.Msg);
            }
        });
    }
    else{
        alert("内容不能留空");
    }
};

function getclassifica(){
    var url="../../Web/Article/GetArticleType"
    $.ajax({
        type: "Get",
        url: url,
        data: "",
        dataType: "Json",
        success: function (data) {
            var mydata=data.Data;
            count = mydata.length;
            var str="";
            for (var i = 0; i < count; i++) {
                str += "<label><input name='Fruit' type='radio' value=" 
                + mydata[i].Id + " " + "/>" + mydata[i].TypeName + "</label>"
            };
            $(".article-classification").append(str);

        },
        error: function () {
            alert("检查网络");
        }
    });
}

