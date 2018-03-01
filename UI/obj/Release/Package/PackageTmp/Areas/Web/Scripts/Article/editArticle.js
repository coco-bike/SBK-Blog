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

    //获取分类成功之后获取文章内容
    getclassifica();


});

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
            //获取文章内容
            gethtmlcontent();

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
    var summary = ue.getContentTxt().substring(0,100);
    var titletext = $("#input-title").val();
    var typeid = $("input:radio[name='Fruit']:checked").val();
    var checkbox1 = $("#checkbox-1").is(":checked");
    var articleid = $("#span-id").text();
    $.ajax({
        type: "post",
        url: "../../web/Article/PostEditArticle",
        data: {
            'articleid':articleid,
            'htmltext': content,
            'title': titletext,
            'typeid': typeid,
            'checkbox1': checkbox1,
            'summary':summary
        },
        dataType: "Json",
        success: function (data) {
            alert(data.Msg);
            window.location.href="../../Web/Management/Index"
        }
    });
}

function gethtmlcontent(){
    var articleid = $("#span-id").text();
    $.ajax({
        type: "Post",
        url: "../../Web/Article/GetEditArticleContent",
        data: {'id':articleid},
        dataType: "Json",
        success: function (data) {
            var mydata = new Array();
            mydata=data.Data;
            var ue = UE.getEditor('container');
            ue.ready(function(){
                ue.setContent(mydata.Content);
            });
            if(mydata.State==1){
                var checkbox1text=true;
            }else{
                var checkbox1text=false;
            }
            $("#input-title").val(mydata.Title);
            $("input:radio[name='Fruit']").eq(mydata.BlogTypeId-1).attr("checked","checked");
            $("#checkbox-1").attr("checked",checkbox1text);
        }
    });
}