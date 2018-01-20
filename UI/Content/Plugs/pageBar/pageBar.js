
var page = {
	page_now : 0,
	page_max : 0,
	calculate_page : function(page_new){
		var self = this;
		
		// 如果用户选择的页码和当前不一致，那么做重新计算页码的事情
		if(page_new != self.page_now){
		
			// 计算显示时先将原来的页码全部去掉
			$('#J_page_wrap').html('');
			
			// 当最大页码大于0的时候才需要显示页码
			if(self.page_max > 0){
				var new_page_inner = '';
				
				// 如果当前页码为1，那么不需要首页、尾页和页码跳转功能
				if(self.page_max == 1){
					new_page_inner = '<a href="javascript:;" data="'+page_new+'" class="cur">'+page_new+'</a><span>共 '+ self.page_max +' 页</span>';
					
				// 不为1的时候，需要首页、尾页和页码跳转功能
				}else{
					new_page_inner = '<a href="javascript:;" data="'+page_new+'" class="cur">'+page_new+'</a>';
					var temp_page;
					
					// 1-3 做循环，				
					for(var i = 1;i <= 3; i++){
						// 计算当前页面之前三页，把真实存在的页码加在当前页码之前（即如果计算出来的页码大于0，则认为是真实存在）
						temp_page = page_new - i;
						if(temp_page > 0){
							new_page_inner = '<a href="javascript:;" data="'+temp_page+'">'+temp_page+'</a>' + new_page_inner;
						}
						
						// 计算当前页面之后三页，把真实存在的页码加在当前页码之后（即如果计算出来的页码小于等于最大页码，则认为是真实存在）
						temp_page = page_new - - i;
						if(temp_page <= self.page_max){
							new_page_inner = new_page_inner + '<a href="javascript:;" data="'+temp_page+'">'+temp_page+'</a>';
						}
					}
					
					// 将页码，首页，尾页，跳转页码组装起来
					new_page_inner = '<a href="javascript:;" data="1" >首页</a>' + new_page_inner + '<a href="javascript:;" data="'+ self.page_max +'">尾页</a><span>共 '+ self.page_max +' 页</span><span>跳转到第<input id="J_page_input" type="text" value="" autocomplete="off" />页</span><a href="javascript:;" type="go" id="J_go_to_page">确定</a>';
				}
				
				// 将组装好的页码代码块放到页码的容器中
				$('#J_page_wrap').html(new_page_inner);
			}
			
			self.page_now = page_new;
		}
	},
	go_to_page : function(){
		var self = this;
		
		// 正则表达式，用来做匹配，确保输入的是否是整数
		var r = /^[-+]?\d*$/;
		
		// 获取用户输入的页码值，需要用.trim函数把用户输入的最前面和最后面的空格去掉
		var page_input = $.trim($('#J_page_input').val());
		
		// 判断是否为整数
		if(r.test(page_input)){
		
			// 如果输入的页码小于等于0，那么把数字纠正为1
			if(page_input <= 0){
				page_input = 1;
				
			// 如果输入的页码大于最大页码，那么把数字纠正为最大页码
			}else if(page_input > self.page_max){
				page_input = self.page_max;
			}
			
			// 如果用户输入的页码和当前不一致，那么做重新计算页码的事情
			if(page_input != self.page_now){
				self.calculate_page(page_input);
			}
		}else{
			alert('请输入整数');
		}
	}
}

// 除了页码跳转的按钮，其它按钮都进行下面的操作（首页、尾页和页码数）
$('#J_page_wrap').on('click','a[id!="J_go_to_page"]',function(){
	var new_page = $(this).attr('data');
	getMessage(new_page);
	page.calculate_page(new_page);
});

// 页码跳转按钮
$('#J_page_wrap').on('click','#J_go_to_page',function(){
	var new_page=$("#J_page_input").val();
	getMessage(new_page);
	page.go_to_page();
});

// 页码输入框，如果用户按下回车键时，也触发和页码跳转按钮一样的效果
$('#J_page_wrap').on('keydown','#J_page_input',function(e){
	if( e.keyCode == 13 ){
		var new_page=$('#J_page_input').val();
		getMessage(new_page);
		page.go_to_page();
	}
});
