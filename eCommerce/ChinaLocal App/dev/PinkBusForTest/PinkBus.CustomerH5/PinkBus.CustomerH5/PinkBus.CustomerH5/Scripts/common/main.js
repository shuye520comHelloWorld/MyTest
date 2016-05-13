/*global $:false,jQuery:false */
'use strict';
var rs = function () {
    var dw = document.documentElement.clientWidth;
    $('body').css({ 'font-size': dw / 320 + 'em' });
};
$(function(){rs();});
$(window).resize(function(){rs();});
(function($){
  function Showselect(dateArray,multi){
    this.handlers = [];
    this.dateArray = dateArray;
    this.ismulti = multi?multi:false;
    this.init();
  }
  Showselect.prototype = {
    constructor : Showselect,
    on: function (type, handler) {
      if(typeof this.handlers[type] === 'undefined'){
        this.handlers[type] = [];
      }
      this.handlers[type].push(handler);
    },
    fire: function (type, data) {
      if(this.handlers[type] instanceof  Array){
        var handlers = this.handlers[type];
        for(var i=0,len = handlers.length;i<len;i++){
          handlers[i](data);
        }
      }
    },
    render:function(){
      var context = this;
      context.cancleBtn = $('<button class="pure-button cancle-btn">取消</button>');
      context.submitBtn = $('<button class="pure-button submit-btn">确认</button>');
      context.boundingbox = $('<div class="select-section"></div>');
      context.boundingboxheader = $('<div class="select-section-header">' +
        '请选择' +
        '</div>');
      context.boundingboxbody = $('<div class="select-section-body"></div>');
      context.boundingboxbottom = $('<div class="select-section-bottom"></div>');

      context.dateArray.map(function (data) {          
          var item = $('<div  class="select-section-item" data-index="' + data.value + '">' + data.text + '<span class="check"></span></div>');
        context.boundingboxbody.append(item);
      });

      context.boundingboxbottom.append(context.cancleBtn).append(context.submitBtn);
      context.$div = context.boundingbox.append(context.boundingboxheader)
        .append(context.boundingboxbody)
        .append(context.boundingboxbottom);

      $('body').append(context.$div);
      return context;
    },
    bindUI: function () {
      var context = this;
      var selectIndexs = [];
      context.submitBtn.on('click', function () {
        context.boundingboxbody.find('.checked').each(function(){
            selectIndexs.push($(this).data('index'));
        });
        context.fire('changed',selectIndexs);
        selectIndexs = [];
        context.hide();
      });
      context.cancleBtn.on('click', function () {
        context.fire('cancle');
        context.hide();
      });
      context.boundingboxbody.on('click','.select-section-item',function(){
        $(this).toggleClass( 'checked');
        if(!context.ismulti){
          $(this).siblings().removeClass('checked');
        }
      });
      return this;
    },
    show:function(){
      var $selectBg;
      if($('#selectBg').length>0){
        $('#selectBg').show();
      }else{
        $selectBg = $('<div id="selectBg"></div>');
        $selectBg.appendTo($('body'));
      }
      this.$div.show();
      return this;
    },
    hide: function () {
      $('#selectBg').hide();
      this.$div.hide();
    },
    init:function(){
      this.render().bindUI().hide();
    }
  };

  function infoSelect(dateArray,multi){
    var $this = $(this);
    var $thisHtml = $this.html();
    var showselect = new Showselect(dateArray,multi);
    var selected = [];   
    showselect.on('changed', function (data) {
        console.log(data + "---");
        console.log(dateArray);
        
        data.map(function (v) {
            $.each(dateArray,function (index, element) {
                if (element.value===v) {
                    selected.push(element);
                }               
            });
        });
        console.log($this);
       
        $this.siblings('input').val(            
            selected.map(function (data) { console.log(data); return data.value; }).join(',')
            );
    if($this.siblings('input').is('.required')){
      if ('createEvent' in document) {
          var evt = document.createEvent('HTMLEvents');
          evt.initEvent('change', false, true);
          $this.siblings('input')[0].dispatchEvent(evt);
      }else{
          $this.siblings('input')[0].fireEvent('onchange');
      }
    }
    console.log(selected );
    if(selected.length>0){
        $this.html(selected.map(function (data) { console.log(data); return data.text;}).join(' '));
      $this.toggleClass('info-select-placeholer',false);
    }else{
      $this.html($thisHtml);
      $this.toggleClass('info-select-placeholer',true);
    }
    selected = [];
    });

    $this.on('click', function (e) {      
      showselect.show();
    });

  }

  $.fn.infoSelect = function(selectDate){
    this.each(function(){
      var selfId = $(this).attr('id');
      if(!selectDate[selfId]){return false;}
      if(selectDate.multi && selectDate.multi.indexOf(selfId)>-1){
        infoSelect.call(this,selectDate[selfId],true);
      }else{
        infoSelect.call(this,selectDate[selfId],false);
      }
    });
  };

}(jQuery));
(function($){

  function alert(words){
    var alertDiv = $('#alertDiv');
    if(alertDiv.length === 1 ){return false;}
    alertDiv = $('<div class="lc-alert" id="alertDiv"> </div>');
    alertDiv.html(words);
    $('body').append(alertDiv);
    setTimeout($.proxy(alertDiv.remove,alertDiv),3000);
  }
  $.alert = alert;
}(jQuery));
(function (window) {
    function Loading() {
        this.dom = $('<div class="lc-loading"><div class="lc-loading-gif"></div><div class="lc-loaidng-words">邀请券加载中，请稍后...</div></div>');
        $('body').append(this.dom)
    }
    Loading.prototype = {
        destory: function () {
            this.dom.remove()
        }
    };

    window.Loading = Loading;

}(window));

//var selectDate = {
//  'multi':['topic','course','product','content'], //可以多选指示器,
//  'identity':['身份一','身份二','身份三','身份四'],
//  'profession':['职业一','职业二','职业三','职业四','职业五','职业六'],
//  'topic':['话题一','话题二','话题三','话题四'],
//  'course':['课程一','课程二','课程三','课程四'],
//  'product':['产品一','产品二','产品三','产品四'],
//  'content':['内容一','内容二','内容三','内容四']
//};

//$('.info-select').infoSelect(selectDate);

$('.required').on('change keypress',function(){
  var i = 0;
  $('.required').each(function(){
    if($(this).val().length>0){
      i = i + 1;
    }
    if(i === 4){
      $('.info-form-submit .pure-button').removeClass('disable');
    }else{
      $('.info-form-submit .pure-button').addClass('disable');
    }
  });
});


