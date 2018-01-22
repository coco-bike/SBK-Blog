$(function(){

    //初始化
    var ue = UE.getEditor('container');

    //绑定保存的页面事件
    $("#submit").click(function(){
        posthtml();
    });

    //绑定清空点击事件
    $("#cancel").click(function () {
        cancelhtml();
    });

    //获取分类
    getclassifica();

    //获取html文本
    gethtmlcontent();

});

function getclassifica(){
    var url="../../../Article/GetArticleType"
    $.ajax({
        type: "Get",
        url: url,
        data: "",
        dataType: "Json",
        success: function (data) {
            count = data.length;
            var str="";
            for (var i = 0; i < count; i++) {
                str = "<label><input name='Fruit' type='radio' value=" 
                + data[i].valueid + " " + "/>" + data[i].name + "</label>"
            };
            $(".article-classification").append(str);
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
    var checkbox2 = $("#checkbox-1").is(":checked");
    $.ajax({
        type: "post",
        url: "../../Article/PostEditArticle",
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

function gethtmlcontent(){
    var articleid = $("#span-id").text();
    $.ajax({
        type: "Post",
        url: "../../../Article/GetEditArticleContent",
        data: {'id':articleid},
        dataType: "Json",
        success: function (data) {
            var ue = UE.getEditor('container');
            ue.setContent(data.Content);
            if(data.State==1){
                var checkbox1text=true;
            }else{
                var checkbox1text=false;
            }
            $("#input-title").val()=data.Title;
            $("input:radio[name='Fruit']").eq(data.Typeid-1).attr("checked","checked");
            $("#checkbox-1").attr("checked",checkbox1text);
            $("#checkbox-2").attr("checked",data.CommitState)
        }
    });
}