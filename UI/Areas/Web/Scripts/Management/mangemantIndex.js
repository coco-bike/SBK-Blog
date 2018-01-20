var max = 0;
var size = 5;
var page_now = 1;

/*
 * Page Demo v 1.0.0
 * Copyright 2015-08-03 Jane
 * page_now 当前页码，初始值为0
 * page_max 最大页码，初始值为0
 * calculate_page(page_new) 重新计算当前的显示，page_new代表新的页码
 * go_to_page() 用户输入页码之后调用的函数，判断用户输入并调用重新计算页码显示
 */

jQuery.support.cors = true;

function getMessage(page_now) {
    $.ajax({
        type: "post",
        url: "  ",
        data: {
            "pagenow": page_now,
            "pagesize": size
        },
        dataType: "Json",
        success: function (data) {
            $('#article-list').empty();
            max = data.totalpage;
            var mydata = new Array();
            mydata = data.listdata;
            var s = "<tr><th>"+mydata[i].title+"</th><th>"+mydata[i].Time+"</th><th>"
            +mydata[i].State+"</th><th>"+mydata[i].WatchCount+"</th><th><a"+" "+"href=""+;
            for (var i = 0; i < mydata.length; i++) {
                s += "  ";
                $('#article-list').append(s);
                s = "";
            }
            page.page_max = max;
            page.calculate_page(page_now); //page.calculate_page(当前页)
        }
    });
};

$(function(){
    //获取文章列表
    getMessage(page_now);
})