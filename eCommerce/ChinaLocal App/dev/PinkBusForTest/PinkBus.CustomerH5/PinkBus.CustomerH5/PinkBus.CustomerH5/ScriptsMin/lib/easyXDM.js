/**
 * easyXDM
 * http://easyxdm.net/
 * Copyright(c) 2009-2011, Øyvind Sean Kinsey, oyvind@kinsey.no.
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

// Copyright(c) 2009-2011, Øyvind Sean Kinsey, oyvind@kinsey.no.

// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell

// The above copyright notice and this permission notice shall be included in

// Copyright(c) 2009-2011, Øyvind Sean Kinsey, oyvind@kinsey.no.

// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell

// The above copyright notice and this permission notice shall be included in

// Copyright(c) 2009-2011, Øyvind Sean Kinsey, oyvind@kinsey.no.

// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell

// The above copyright notice and this permission notice shall be included in

// Copyright(c) 2009-2011, Øyvind Sean Kinsey, oyvind@kinsey.no.

// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell

// The above copyright notice and this permission notice shall be included in

// Copyright(c) 2009-2011, Øyvind Sean Kinsey, oyvind@kinsey.no.

// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell

// The above copyright notice and this permission notice shall be included in

// Copyright(c) 2009-2011, Øyvind Sean Kinsey, oyvind@kinsey.no.

// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell

// The above copyright notice and this permission notice shall be included in

// Copyright(c) 2009-2011, Øyvind Sean Kinsey, oyvind@kinsey.no.

// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell

// The above copyright notice and this permission notice shall be included in

// Copyright(c) 2009-2011, Øyvind Sean Kinsey, oyvind@kinsey.no.

// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell

// The above copyright notice and this permission notice shall be included in

// Copyright(c) 2009-2011, Øyvind Sean Kinsey, oyvind@kinsey.no.

// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell

// The above copyright notice and this permission notice shall be included in

// Copyright(c) 2009-2011, Øyvind Sean Kinsey, oyvind@kinsey.no.

// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell

// The above copyright notice and this permission notice shall be included in

// Copyright(c) 2009-2011, Øyvind Sean Kinsey, oyvind@kinsey.no.

// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell

// The above copyright notice and this permission notice shall be included in

// Copyright(c) 2009-2011, Øyvind Sean Kinsey, oyvind@kinsey.no.

// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell

// The above copyright notice and this permission notice shall be included in

// Copyright(c) 2009-2011, Øyvind Sean Kinsey, oyvind@kinsey.no.

// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell

// The above copyright notice and this permission notice shall be included in

// Copyright(c) 2009-2011, Øyvind Sean Kinsey, oyvind@kinsey.no.

// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell

// The above copyright notice and this permission notice shall be included in

// Copyright(c) 2009-2011, Øyvind Sean Kinsey, oyvind@kinsey.no.

// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell

// The above copyright notice and this permission notice shall be included in

// Copyright(c) 2009-2011, Øyvind Sean Kinsey, oyvind@kinsey.no.

// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell

// The above copyright notice and this permission notice shall be included in

(function(a,b,c,d,e,f){function g(a,b){var c=typeof a[b];return c=="function"||c=="object"&&!!a[b]||c=="unknown"}function h(a,b){return typeof a[b]=="object"&&!!a[b]}function i(a){return Object.prototype.toString.call(a)==="[object Array]"}function j(){var a="Shockwave Flash",b="application/x-shockwave-flash";if(!t(navigator.plugins)&&typeof navigator.plugins[a]=="object"){var c=navigator.plugins[a].description;c&&!t(navigator.mimeTypes)&&navigator.mimeTypes[b]&&navigator.mimeTypes[b].enabledPlugin&&(N=c.match(/\d+/g))}if(!N){var d;try{d=new ActiveXObject("ShockwaveFlash.ShockwaveFlash"),N=Array.prototype.slice.call(d.GetVariable("$version").match(/(\d+),(\d+),(\d+),(\d+)/),1),d=null}catch(e){}}if(!N)return!1;var f=parseInt(N[0],10),g=parseInt(N[1],10);return O=f>9&&g>0,!0}function k(){if(R)return;R=!0;for(var a=0;a<S.length;a++)S[a]();S.length=0}function l(a,b){if(R){a.call(b);return}S.push(function(){a.call(b)})}function m(){var a=parent;if(H!=="")for(var b=0,c=H.split(".");b<c.length;b++)a=a[c[b]];return a.easyXDM}function n(b){return a.easyXDM=J,H=b,H&&(K="easyXDM_"+H.replace(".","_")+"_"),I}function o(a){return a.match(E)[3]}function p(a){return a.match(E)[4]||""}function q(a){var b=a.toLowerCase().match(E),c=b[2],d=b[3],e=b[4]||"";if(c=="http:"&&e==":80"||c=="https:"&&e==":443")e="";return c+"//"+d+e}function r(a){a=a.replace(G,"$1/");if(!a.match(/^(http||https):\/\//)){var b=a.substring(0,1)==="/"?"":c.pathname;b.substring(b.length-1)!=="/"&&(b=b.substring(0,b.lastIndexOf("/")+1)),a=c.protocol+"//"+c.host+b+a}while(F.test(a))a=a.replace(F,"");return a}function s(a,b){var c="",d=a.indexOf("#");d!==-1&&(c=a.substring(d),a=a.substring(0,d));var e=[];for(var g in b)b.hasOwnProperty(g)&&e.push(g+"="+f(b[g]));return a+(M?"#":a.indexOf("?")==-1?"?":"&")+e.join("&")+c}function t(a){return typeof a=="undefined"}function u(a,b,c){var d;for(var e in b)b.hasOwnProperty(e)&&(e in a?(d=b[e],typeof d=="object"?u(a[e],d,c):c||(a[e]=b[e])):a[e]=b[e]);return a}function v(){var a=b.body.appendChild(b.createElement("form")),c=a.appendChild(b.createElement("input"));c.name=K+"TEST"+C,L=c!==a.elements[c.name],b.body.removeChild(a)}function w(a){t(L)&&v();var c;L?c=b.createElement('<iframe name="'+a.props.name+'"/>'):(c=b.createElement("IFRAME"),c.name=a.props.name),c.id=c.name=a.props.name,delete a.props.name,typeof a.container=="string"&&(a.container=b.getElementById(a.container)),a.container||(u(c.style,{position:"absolute",top:"-2000px",left:"0px"}),a.container=b.body);var d=a.props.src;a.props.src="javascript:false",u(c,a.props),c.border=c.frameBorder=0,c.allowTransparency=!0,a.container.appendChild(c),a.onLoad&&P(c,"load",a.onLoad);if(a.usePost){var e=a.container.appendChild(b.createElement("form")),f;e.target=c.name,e.action=d,e.method="POST";if(typeof a.usePost=="object")for(var g in a.usePost)a.usePost.hasOwnProperty(g)&&(L?f=b.createElement('<input name="'+g+'"/>'):(f=b.createElement("INPUT"),f.name=g),f.value=a.usePost[g],e.appendChild(f));e.submit(),e.parentNode.removeChild(e)}else c.src=d;return a.props.src=d,c}function x(a,b){typeof a=="string"&&(a=[a]);var c,d=a.length;while(d--){c=a[d],c=new RegExp(c.substr(0,1)=="^"?c:"^"+c.replace(/(\*)/g,".$1").replace(/\?/g,".")+"$");if(c.test(b))return!0}return!1}function y(d){var e=d.protocol,f;d.isHost=d.isHost||t(V.xdm_p),M=d.hash||!1,d.props||(d.props={});if(!d.isHost){d.channel=V.xdm_c.replace(/["'<>\\]/g,""),d.secret=V.xdm_s,d.remote=V.xdm_e.replace(/["'<>\\]/g,""),e=V.xdm_p;if(d.acl&&!x(d.acl,d.remote))throw new Error("Access denied for "+d.remote)}else d.remote=r(d.remote),d.channel=d.channel||"default"+C++,d.secret=Math.random().toString(16).substring(2),t(e)&&(q(c.href)==q(d.remote)?e="4":g(a,"postMessage")||g(b,"postMessage")?e="1":d.swf&&g(a,"ActiveXObject")&&j()?e="6":navigator.product==="Gecko"&&"frameElement"in a&&navigator.userAgent.indexOf("WebKit")==-1?e="5":d.remoteHelper?e="2":e="0");d.protocol=e;switch(e){case"0":u(d,{interval:100,delay:2e3,useResize:!0,useParent:!1,usePolling:!1},!0);if(d.isHost){if(!d.local){var h=c.protocol+"//"+c.host,i=b.body.getElementsByTagName("img"),k,l=i.length;while(l--){k=i[l];if(k.src.substring(0,h.length)===h){d.local=k.src;break}}d.local||(d.local=a)}var m={xdm_c:d.channel,xdm_p:0};d.local===a?(d.usePolling=!0,d.useParent=!0,d.local=c.protocol+"//"+c.host+c.pathname+c.search,m.xdm_e=d.local,m.xdm_pa=1):m.xdm_e=r(d.local),d.container&&(d.useResize=!1,m.xdm_po=1),d.remote=s(d.remote,m)}else u(d,{channel:V.xdm_c,remote:V.xdm_e,useParent:!t(V.xdm_pa),usePolling:!t(V.xdm_po),useResize:d.useParent?!1:d.useResize});f=[new I.stack.HashTransport(d),new I.stack.ReliableBehavior({}),new I.stack.QueueBehavior({encode:!0,maxLength:4e3-d.remote.length}),new I.stack.VerifyBehavior({initiate:d.isHost})];break;case"1":f=[new I.stack.PostMessageTransport(d)];break;case"2":d.isHost&&(d.remoteHelper=r(d.remoteHelper)),f=[new I.stack.NameTransport(d),new I.stack.QueueBehavior,new I.stack.VerifyBehavior({initiate:d.isHost})];break;case"3":f=[new I.stack.NixTransport(d)];break;case"4":f=[new I.stack.SameOriginTransport(d)];break;case"5":f=[new I.stack.FrameElementTransport(d)];break;case"6":N||j(),f=[new I.stack.FlashTransport(d)]}return f.push(new I.stack.QueueBehavior({lazy:d.lazy,remove:!0})),f}function z(a){var b,c={incoming:function(a,b){this.up.incoming(a,b)},outgoing:function(a,b){this.down.outgoing(a,b)},callback:function(a){this.up.callback(a)},init:function(){this.down.init()},destroy:function(){this.down.destroy()}};for(var d=0,e=a.length;d<e;d++)b=a[d],u(b,c,!0),d!==0&&(b.down=a[d-1]),d!==e-1&&(b.up=a[d+1]);return b}function A(a){a.up.down=a.down,a.down.up=a.up,a.up=a.down=null}var B=this,C=Math.floor(Math.random()*1e4),D=Function.prototype,E=/^((http.?:)\/\/([^:\/\s]+)(:\d+)*)/,F=/[\-\w]+\/\.\.\//,G=/([^:])\/\//g,H="",I={},J=a.easyXDM,K="easyXDM_",L,M=!1,N,O,P,Q;if(g(a,"addEventListener"))P=function(a,b,c){a.addEventListener(b,c,!1)},Q=function(a,b,c){a.removeEventListener(b,c,!1)};else{if(!g(a,"attachEvent"))throw new Error("Browser not supported");P=function(a,b,c){a.attachEvent("on"+b,c)},Q=function(a,b,c){a.detachEvent("on"+b,c)}}var R=!1,S=[],T;"readyState"in b?(T=b.readyState,R=T=="complete"||~navigator.userAgent.indexOf("AppleWebKit/")&&(T=="loaded"||T=="interactive")):R=!!b.body;if(!R){if(g(a,"addEventListener"))P(b,"DOMContentLoaded",k);else{P(b,"readystatechange",function(){b.readyState=="complete"&&k()});if(b.documentElement.doScroll&&a===top){var U=function(){if(R)return;try{b.documentElement.doScroll("left")}catch(a){d(U,1);return}k()};U()}}P(a,"load",k)}var V=function(a){a=a.substring(1).split("&");var b={},c,d=a.length;while(d--)c=a[d].split("="),b[c[0]]=e(c[1]);return b}(/xdm_e=/.test(c.search)?c.search:c.hash),W=function(){var a={},b={a:[1,2,3]},c='{"a":[1,2,3]}';return typeof JSON!="undefined"&&typeof JSON.stringify=="function"&&JSON.stringify(b).replace(/\s/g,"")===c?JSON:(Object.toJSON&&Object.toJSON(b).replace(/\s/g,"")===c&&(a.stringify=Object.toJSON),typeof String.prototype.evalJSON=="function"&&(b=c.evalJSON(),b.a&&b.a.length===3&&b.a[2]===3&&(a.parse=function(a){return a.evalJSON()})),a.stringify&&a.parse?(W=function(){return a},a):null)};u(I,{version:"2.4.19.3",query:V,stack:{},apply:u,getJSONObject:W,whenReady:l,noConflict:n}),I.DomHelper={on:P,un:Q,requiresJSON:function(c){h(a,"JSON")||b.write('<script type="text/javascript" src="'+c+'"><'+"/script>")}},function(){var a={};I.Fn={set:function(b,c){a[b]=c},get:function(b,c){if(!a.hasOwnProperty(b))return;var d=a[b];return c&&delete a[b],d}}}(),I.Socket=function(a){var b=z(y(a).concat([{incoming:function(b,c){a.onMessage(b,c)},callback:function(b){a.onReady&&a.onReady(b)}}])),c=q(a.remote);this.origin=q(a.remote),this.destroy=function(){b.destroy()},this.postMessage=function(a){b.outgoing(a,c)},b.init()},I.Rpc=function(a,b){if(b.local)for(var c in b.local)if(b.local.hasOwnProperty(c)){var d=b.local[c];typeof d=="function"&&(b.local[c]={method:d})}var e=z(y(a).concat([new I.stack.RpcBehavior(this,b),{callback:function(b){a.onReady&&a.onReady(b)}}]));this.origin=q(a.remote),this.destroy=function(){e.destroy()},e.init()},I.stack.SameOriginTransport=function(a){var b,e,f,g;return b={outgoing:function(a,b,c){f(a),c&&c()},destroy:function(){e&&(e.parentNode.removeChild(e),e=null)},onDOMReady:function(){g=q(a.remote),a.isHost?(u(a.props,{src:s(a.remote,{xdm_e:c.protocol+"//"+c.host+c.pathname,xdm_c:a.channel,xdm_p:4}),name:K+a.channel+"_provider"}),e=w(a),I.Fn.set(a.channel,function(a){return f=a,d(function(){b.up.callback(!0)},0),function(a){b.up.incoming(a,g)}})):(f=m().Fn.get(a.channel,!0)(function(a){b.up.incoming(a,g)}),d(function(){b.up.callback(!0)},0))},init:function(){l(b.onDOMReady,b)}}},I.stack.FlashTransport=function(a){function e(a,b){d(function(){h.up.incoming(a,k)},0)}function g(c){var d=a.swf+"?host="+a.isHost,e="easyXDM_swf_"+Math.floor(Math.random()*1e4);I.Fn.set("flash_loaded"+c.replace(/[\-.]/g,"_"),function(){I.stack.FlashTransport[c].swf=m=n.firstChild;var a=I.stack.FlashTransport[c].queue;for(var b=0;b<a.length;b++)a[b]();a.length=0}),a.swfContainer?n=typeof a.swfContainer=="string"?b.getElementById(a.swfContainer):a.swfContainer:(n=b.createElement("div"),u(n.style,O&&a.swfNoThrottle?{height:"20px",width:"20px",position:"fixed",right:0,top:0}:{height:"1px",width:"1px",position:"absolute",overflow:"hidden",right:0,top:0}),b.body.appendChild(n));var g="callback=flash_loaded"+f(c.replace(/[\-.]/g,"_"))+"&proto="+B.location.protocol+"&domain="+f(o(B.location.href))+"&port="+f(p(B.location.href))+"&ns="+f(H);n.innerHTML="<object height='20' width='20' type='application/x-shockwave-flash' id='"+e+"' data='"+d+"'>"+"<param name='allowScriptAccess' value='always'></param>"+"<param name='wmode' value='transparent'>"+"<param name='movie' value='"+d+"'></param>"+"<param name='flashvars' value='"+g+"'></param>"+"<embed type='application/x-shockwave-flash' FlashVars='"+g+"' allowScriptAccess='always' wmode='transparent' src='"+d+"' height='1' width='1'></embed>"+"</object>"}var h,i,j,k,m,n;return h={outgoing:function(b,c,d){m.postMessage(a.channel,b.toString()),d&&d()},destroy:function(){try{m.destroyChannel(a.channel)}catch(b){}m=null,i&&(i.parentNode.removeChild(i),i=null)},onDOMReady:function(){k=a.remote,I.Fn.set("flash_"+a.channel+"_init",function(){d(function(){h.up.callback(!0)})}),I.Fn.set("flash_"+a.channel+"_onMessage",e),a.swf=r(a.swf);var b=o(a.swf),f=function(){I.stack.FlashTransport[b].init=!0,m=I.stack.FlashTransport[b].swf,m.createChannel(a.channel,a.secret,q(a.remote),a.isHost),a.isHost&&(O&&a.swfNoThrottle&&u(a.props,{position:"fixed",right:0,top:0,height:"20px",width:"20px"}),u(a.props,{src:s(a.remote,{xdm_e:q(c.href),xdm_c:a.channel,xdm_p:6,xdm_s:a.secret}),name:K+a.channel+"_provider"}),i=w(a))};I.stack.FlashTransport[b]&&I.stack.FlashTransport[b].init?f():I.stack.FlashTransport[b]?I.stack.FlashTransport[b].queue.push(f):(I.stack.FlashTransport[b]={queue:[f]},g(b))},init:function(){l(h.onDOMReady,h)}}},I.stack.PostMessageTransport=function(b){function e(a){if(a.origin)return q(a.origin);if(a.uri)return q(a.uri);if(a.domain)return c.protocol+"//"+a.domain;throw"Unable to retrieve the origin of the event"}function f(a){var c=e(a);c==j&&a.data.substring(0,b.channel.length+1)==b.channel+" "&&g.up.incoming(a.data.substring(b.channel.length+1),c)}var g,h,i,j;return g={outgoing:function(a,c,d){i.postMessage(b.channel+" "+a,c||j),d&&d()},destroy:function(){Q(a,"message",f),h&&(i=null,h.parentNode.removeChild(h),h=null)},onDOMReady:function(){j=q(b.remote);if(b.isHost){var e=function(c){c.data==b.channel+"-ready"&&(i="postMessage"in h.contentWindow?h.contentWindow:h.contentWindow.document,Q(a,"message",e),P(a,"message",f),d(function(){g.up.callback(!0)},0))};P(a,"message",e),u(b.props,{src:s(b.remote,{xdm_e:q(c.href),xdm_c:b.channel,xdm_p:1}),name:K+b.channel+"_provider"}),h=w(b)}else P(a,"message",f),i="postMessage"in a.parent?a.parent:a.parent.document,i.postMessage(b.channel+"-ready",j),d(function(){g.up.callback(!0)},0)},init:function(){l(g.onDOMReady,g)}}},I.stack.FrameElementTransport=function(e){var f,g,h,i;return f={outgoing:function(a,b,c){h.call(this,a),c&&c()},destroy:function(){g&&(g.parentNode.removeChild(g),g=null)},onDOMReady:function(){i=q(e.remote),e.isHost?(u(e.props,{src:s(e.remote,{xdm_e:q(c.href),xdm_c:e.channel,xdm_p:5}),name:K+e.channel+"_provider"}),g=w(e),g.fn=function(a){return delete g.fn,h=a,d(function(){f.up.callback(!0)},0),function(a){f.up.incoming(a,i)}}):(b.referrer&&q(b.referrer)!=V.xdm_e&&(a.top.location=V.xdm_e),h=a.frameElement.fn(function(a){f.up.incoming(a,i)}),f.up.callback(!0))},init:function(){l(f.onDOMReady,f)}}},I.stack.NameTransport=function(a){function b(b){var c=a.remoteHelper+(h?"#_3":"#_2")+a.channel;i.contentWindow.sendMessage(b,c)}function c(){h?(++k===2||!h)&&g.up.callback(!0):(b("ready"),g.up.callback(!0))}function e(a){g.up.incoming(a,n)}function f(){m&&d(function(){m(!0)},0)}var g,h,i,j,k,m,n,o;return g={outgoing:function(a,c,d){m=d,b(a)},destroy:function(){i.parentNode.removeChild(i),i=null,h&&(j.parentNode.removeChild(j),j=null)},onDOMReady:function(){h=a.isHost,k=0,n=q(a.remote),a.local=r(a.local),h?(I.Fn.set(a.channel,function(b){h&&b==="ready"&&(I.Fn.set(a.channel,e),c())}),o=s(a.remote,{xdm_e:a.local,xdm_c:a.channel,xdm_p:2}),u(a.props,{src:o+"#"+a.channel,name:K+a.channel+"_provider"}),j=w(a)):(a.remoteHelper=a.remote,I.Fn.set(a.channel,e));var b=function(){var e=i||this;Q(e,"load",b),I.Fn.set(a.channel+"_load",f),function g(){typeof e.contentWindow.sendMessage=="function"?c():d(g,50)}()};i=w({props:{src:a.local+"#_4"+a.channel},onLoad:b})},init:function(){l(g.onDOMReady,g)}}},I.stack.HashTransport=function(b){function c(a){if(!r)return;var c=b.remote+"#"+o++ +"_"+a;(j||!s?r.contentWindow:r).location=c}function e(a){n=a,h.up.incoming(n.substring(n.indexOf("_")+1),t)}function f(){if(!p)return;var a=p.location.href,b="",c=a.indexOf("#");c!=-1&&(b=a.substring(c)),b&&b!=n&&e(b)}function g(){k=setInterval(f,m)}var h,i=this,j,k,m,n,o,p,r,s,t;return h={outgoing:function(a,b){c(a)},destroy:function(){a.clearInterval(k),(j||!s)&&r.parentNode.removeChild(r),r=null},onDOMReady:function(){j=b.isHost,m=b.interval,n="#"+b.channel,o=0,s=b.useParent,t=q(b.remote);if(j){u(b.props,{src:b.remote,name:K+b.channel+"_provider"});if(s)b.onLoad=function(){p=a,g(),h.up.callback(!0)};else{var c=0,e=b.delay/50;(function f(){if(++c>e)throw new Error("Unable to reference listenerwindow");try{p=r.contentWindow.frames[K+b.channel+"_consumer"]}catch(a){}p?(g(),h.up.callback(!0)):d(f,50)})()}r=w(b)}else p=a,g(),s?(r=parent,h.up.callback(!0)):(u(b,{props:{src:b.remote+"#"+b.channel+new Date,name:K+b.channel+"_consumer"},onLoad:function(){h.up.callback(!0)}}),r=w(b))},init:function(){l(h.onDOMReady,h)}}},I.stack.ReliableBehavior=function(a){var b,c,d=0,e=0,f="";return b={incoming:function(a,g){var h=a.indexOf("_"),i=a.substring(0,h).split(",");a=a.substring(h+1),i[0]==d&&(f="",c&&c(!0)),a.length>0&&(b.down.outgoing(i[1]+","+d+"_"+f,g),e!=i[1]&&(e=i[1],b.up.incoming(a,g)))},outgoing:function(a,g,h){f=a,c=h,b.down.outgoing(e+","+ ++d+"_"+a,g)}}},I.stack.QueueBehavior=function(a){function b(){if(a.remove&&g.length===0){A(c);return}if(h||g.length===0||j)return;h=!0;var e=g.shift();c.down.outgoing(e.data,e.origin,function(a){h=!1,e.callback&&d(function(){e.callback(a)},0),b()})}var c,g=[],h=!0,i="",j,k=0,l=!1,m=!1;return c={init:function(){t(a)&&(a={}),a.maxLength&&(k=a.maxLength,m=!0),a.lazy?l=!0:c.down.init()},callback:function(a){h=!1;var d=c.up;b(),d.callback(a)},incoming:function(b,d){if(m){var f=b.indexOf("_"),g=parseInt(b.substring(0,f),10);i+=b.substring(f+1),g===0&&(a.encode&&(i=e(i)),c.up.incoming(i,d),i="")}else c.up.incoming(b,d)},outgoing:function(d,e,h){a.encode&&(d=f(d));var i=[],j;if(m){while(d.length!==0)j=d.substring(0,k),d=d.substring(j.length),i.push(j);while(j=i.shift())g.push({data:i.length+"_"+j,origin:e,callback:i.length===0?h:null})}else g.push({data:d,origin:e,callback:h});l?c.down.init():b()},destroy:function(){j=!0,c.down.destroy()}}},I.stack.VerifyBehavior=function(a){function b(){d=Math.random().toString(16).substring(2),c.down.outgoing(d)}var c,d,e,f=!1;return c={incoming:function(f,g){var h=f.indexOf("_");h===-1?f===d?c.up.callback(!0):e||(e=f,a.initiate||b(),c.down.outgoing(f)):f.substring(0,h)===e&&c.up.incoming(f.substring(h+1),g)},outgoing:function(a,b,e){c.down.outgoing(d+"_"+a,b,e)},callback:function(c){a.initiate&&b()}}},I.stack.RpcBehavior=function(a,b){function c(a){a.jsonrpc="2.0",f.down.outgoing(g.stringify(a))}function d(a,b){var d=Array.prototype.slice;return function(){var e=arguments.length,f,g={method:b};e>0&&typeof arguments[e-1]=="function"?(e>1&&typeof arguments[e-2]=="function"?(f={success:arguments[e-2],error:arguments[e-1]},g.params=d.call(arguments,0,e-2)):(f={success:arguments[e-1]},g.params=d.call(arguments,0,e-1)),j[""+ ++h]=f,g.id=h):g.params=d.call(arguments,0),a.namedParams&&g.params.length===1&&(g.params=g.params[0]),c(g)}}function e(a,b,d,e){if(!d){b&&c({id:b,error:{code:-32601,message:"Procedure not found."}});return}var f,g;b?(f=function(a){f=D,c({id:b,result:a})},g=function(a,d){g=D;var e={id:b,error:{code:-32099,message:a}};d&&(e.error.data=d),c(e)}):f=g=D,i(e)||(e=[e]);try{var h=d.method.apply(d.scope,e.concat([f,g]));t(h)||f(h)}catch(j){g(j.message)}}var f,g=b.serializer||W(),h=0,j={};return f={incoming:function(a,d){var f=g.parse(a);if(f.method)b.handle?b.handle(f,c):e(f.method,f.id,b.local[f.method],f.params);else{var h=j[f.id];f.error?h.error&&h.error(f.error):h.success&&h.success(f.result),delete j[f.id]}},init:function(){if(b.remote)for(var c in b.remote)b.remote.hasOwnProperty(c)&&(a[c]=d(b.remote[c],c));f.down.init()},destroy:function(){for(var c in b.remote)b.remote.hasOwnProperty(c)&&a.hasOwnProperty(c)&&delete a[c];f.down.destroy()}}},B.easyXDM=I})(window,document,location,window.setTimeout,decodeURIComponent,encodeURIComponent)