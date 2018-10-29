/**
 * Created by neil on 2014/6/27.
 * 显示提示框(不依赖于任何框架和库)
 * value：提示框内容
 * type：是否显示读取按钮
 */
(function(win, undefined) {
	win.showLog = function(value, type, time) {
		var maskall = document.getElementById('maskall');
		var mask = document.getElementById('mask');
		if (!maskall) {
			maskall = document.createElement('div');
			maskall.setAttribute('id', 'maskall');
			document.getElementsByTagName('body')[0].appendChild(maskall);
		}
		if (!mask) {
			var span = document.createElement('span'),
				h1 = document.createElement('h1'),
				mask = document.createElement('div');
			mask.setAttribute('id', 'mask');
			mask.setAttribute('class', 'loader_box');
			span.setAttribute('class', 'icon icon_loading spin');

			mask.appendChild(span);
			mask.appendChild(h1);
			document.getElementsByTagName('body')[0].appendChild(mask);
		}
		maskall.style.display = 'block';
		mask.style.display = 'block';
		if (type) mask.getElementsByClassName('spin')[0].style.display = "block";
		else mask.getElementsByClassName('spin')[0].style.display = "none";
		mask.getElementsByTagName('h1')[0].innerHTML = value;


		if (time && typeof time == 'number') {
			setTimeout(function() {
				mask.style.display = 'none';
			}, time);
		}
		this.hide = function() {
			document.getElementById('maskall').style.display = 'none';
			document.getElementById('mask').style.display = 'none';
		};
		this.show = function() {
			document.getElementById('maskall').style.display = 'block';
			document.getElementById('mask').style.display = 'block';
		}

		return this;
	}
})(window)