"use strict";
(function () {
	// Global variables
	var userAgent = navigator.userAgent.toLowerCase(),
			initialDate = new Date(),

			$document = $(document),
			$window = $(window),
			$html = $("html"),
			$body = $("body"),

			isDesktop = $html.hasClass("desktop"),
			isIE = userAgent.indexOf("msie") !== -1 ? parseInt(userAgent.split("msie")[1], 10) : userAgent.indexOf("trident") !== -1 ? 11 : userAgent.indexOf("edge") !== -1 ? 12 : false,
			isMobile = /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent),
			windowReady = false,
			isNoviBuilder = false,
			loaderTimeoutId,
plugins = {
wow:                     $(".wow"),
preloader:               $(".preloader"),
rdMailForm:              $(".rd-meilform"),
rdInputLabel:            $(".form-label"),

			};



if (plugins.preloader.length && !isNoviBuilder) {
			pageTransition({
				target:            document.querySelector('.page'),
				delay:             0,
				duration:          500,
				classIn:           'fadeIn',
				classOut:          'fadeOut',
				classActive:       'animated',
				conditions:        function (event, link) {
					return !/(\#|callto:|tel:|mailto:|:\/\/)/.test(link) && !event.currentTarget.hasAttribute('data-lightgallery');
				},
				onTransitionStart: function (options) {
					setTimeout(function () {
						plugins.preloader.removeClass('loaded');
					}, options.duration * .75);
				},
				onReady:           function () {
					plugins.preloader.addClass('loaded');
					windowReady = true;
				}
			});
		}
	});



// Page loader & Page transition
		if (plugins.preloader.length && !isNoviBuilder) {
			pageTransition({
				target:            document.querySelector('.page'),
				delay:             100,
				duration:          500,
				classIn:           'fadeIn',
				classOut:          'fadeOut',
				classActive:       'animated',
				conditions:        function (event, link) {
					return !/(\#|callto:|tel:|mailto:|:\/\/)/.test(link) && !event.currentTarget.hasAttribute('data-lightgallery');
				},
				onTransitionStart: function (options) {
					setTimeout(function () {
						plugins.preloader.removeClass('loaded');
					}, options.duration * .75);
				},
				onReady:           function () {
					plugins.preloader.addClass('loaded');
					windowReady = true;
				}
			});
		}



		/**
		 * @desc Calls a function when element has been scrolled into the view
		 * @param {object} element - jQuery object
		 * @param {function} func - callback function
		 */
		function lazyInit(element, func) {
			$document.on('scroll', function () {
				if ((!element.hasClass('lazy-loaded') && (isScrolledIntoView(element)))) {
					func.call();
					element.addClass('lazy-loaded');
				}
			}).trigger("scroll");
		}

		/**
		 * @desc Attach form validation to elements
		 * @param {object} elements - jQuery object
		 */
		function attachFormValidator(elements) {
			// Custom validator - phone number
			regula.custom({
				name:           'PhoneNumber',
				defaultMessage: 'Invalid phone number format',
				validator:      function () {
					if (this.value === '') return true;
					else return /^(\+\d)?[0-9\-\(\) ]{5,}$/i.test(this.value);
				}
			});

			for (var i = 0; i < elements.length; i++) {
				var o = $(elements[i]), v;
				o.addClass("form-control-has-validation").after("<span class='form-validation'></span>");
				v = o.parent().find(".form-validation");
				if (v.is(":last-child")) o.addClass("form-control-last-child");
			}

			elements.on('input change propertychange blur', function (e) {
				var $this = $(this), results;

				if (e.type !== "blur") if (!$this.parent().hasClass("has-error")) return;
				if ($this.parents('.rd-meilform').hasClass('success')) return;

				if ((results = $this.regula('validate')).length) {
					for (i = 0; i < results.length; i++) {
						$this.siblings(".form-validation").text(results[i].message).parent().addClass("has-error");
					}
				} else {
					$this.siblings(".form-validation").text("").parent().removeClass("has-error")
				}
			}).regula('bind');

			var regularConstraintsMessages = [
				{
					type:       regula.Constraint.Required,
					newMessage: "The text field is required."
				},
				{
					type:       regula.Constraint.Email,
					newMessage: "The email is not a valid email."
				},
				{
					type:       regula.Constraint.Numeric,
					newMessage: "Only numbers are required"
				},
				{
					type:       regula.Constraint.Selected,
					newMessage: "Please choose an option."
				}
			];


			for (var i = 0; i < regularConstraintsMessages.length; i++) {
				var regularConstraint = regularConstraintsMessages[i];

				regula.override({
					constraintType: regularConstraint.type,
					defaultMessage: regularConstraint.newMessage
				});
			}
		}

		/**
		 * @desc Check if all elements pass validation
		 * @param {object} elements - object of items for validation
		 * @param {object} captcha - captcha object for validation
		 * @return {boolean}
		 */
		function isValidated(elements, captcha) {
			var results, errors = 0;

			if (elements.length) {
				for (var j = 0; j < elements.length; j++) {

					var $input = $(elements[j]);
					if ((results = $input.regula('validate')).length) {
						for (k = 0; k < results.length; k++) {
							errors++;
							$input.siblings(".form-validation").text(results[k].message).parent().addClass("has-error");
						}
					} else {
						$input.siblings(".form-validation").text("").parent().removeClass("has-error")
					}
				}

				if (captcha) {
					if (captcha.length) {
						return validateReCaptcha(captcha) && errors === 0
					}
				}

				return errors === 0;
			}
			return true;
		}



// Page loader
		if (plugins.preloader.length) {
			loaderTimeoutId = setTimeout(function () {
				if (!windowReady && !isNoviBuilder) plugins.preloader.removeClass('loaded');
			}, 2000);
		}




// WOW
		if ($html.hasClass("wow-animation") && plugins.wow.length && !isNoviBuilder && isDesktop) {
			new WOW().init();
		}

	
		
				/**
		 * @desc Validate google reCaptcha
		 * @param {object} captcha - captcha object for validation
		 * @return {boolean}
		 */
				function validateReCaptcha(captcha) {
					var captchaToken = captcha.find('.g-recaptcha-response').val();
		
					if (captchaToken.length === 0) {
						captcha
						.siblings('.form-validation')
						.html('Please, prove that you are not robot.')
						.addClass('active');
						captcha
						.closest('.form-wrap')
						.addClass('has-error');
		
						captcha.on('propertychange', function () {
							var $this = $(this),
									captchaToken = $this.find('.g-recaptcha-response').val();
		
							if (captchaToken.length > 0) {
								$this
								.closest('.form-wrap')
								.removeClass('has-error');
								$this
								.siblings('.form-validation')
								.removeClass('active')
								.html('');
								$this.off('propertychange');
							}
						});
		
						return false;
					}
		
					return true;
				}




				
					// Swiper
		if (plugins.swiper.length) {
			for (var i = 0; i < plugins.swiper.length; i++) {
				var s = $(plugins.swiper[i]);
				var pag = s.find(".swiper-pagination"),
						next = s.find(".swiper-button-next"),
						prev = s.find(".swiper-button-prev"),
						bar = s.find(".swiper-scrollbar"),
						swiperSlide = s.find(".swiper-slide"),
						autoplay = false;

				for (var j = 0; j < swiperSlide.length; j++) {
					var $this = $(swiperSlide[j]),
							url;

					if (url = $this.attr("data-slide-bg")) {
						$this.css({
							"background-image": "url(" + url + ")",
							"background-size":  "cover"
						})
					}
				}

				swiperSlide.end()
				.find("[data-caption-animate]")
				.addClass("not-animated")
				.end();

				s.swiper({
					autoplay:                 s.attr('data-autoplay') ? s.attr('data-autoplay') === "false" ? undefined : s.attr('data-autoplay') : 5000,
					direction:                s.attr('data-direction') && isDesktop ? s.attr('data-direction') : "horizontal",
					effect:                   s.attr('data-slide-effect') ? s.attr('data-slide-effect') : "slide",
					speed:                    s.attr('data-slide-speed') ? s.attr('data-slide-speed') : 600,
					keyboardControl:          s.attr('data-keyboard') === "true",
					mousewheelControl:        s.attr('data-mousewheel') === "true",
					mousewheelReleaseOnEdges: s.attr('data-mousewheel-release') === "true",
					nextButton:               next.length ? next.get(0) : null,
					prevButton:               prev.length ? prev.get(0) : null,
					pagination:               pag.length ? pag.get(0) : null,
					paginationClickable:      pag.length ? pag.attr("data-clickable") !== "false" : false,
					paginationBulletRender:   function (swiper, index, className) {
						if (pag.attr("data-index-bullet") === "true") {
							return '<span class="' + className + '">' + (index + 1) + '</span>';
						} else if (pag.attr("data-bullet-custom") === "true") {
							return '<span class="' + className + '"><span></span></span>';
						} else {
							return '<span class="' + className + '"></span>';
						}
					},
					scrollbar:                bar.length ? bar.get(0) : null,
					scrollbarDraggable:       bar.length ? bar.attr("data-draggable") !== "false" : true,
					scrollbarHide:            bar.length ? bar.attr("data-draggable") === "false" : false,
					loop:                     isNoviBuilder ? false : s.attr('data-loop') !== "false",
					simulateTouch:            s.attr('data-simulate-touch') && !isNoviBuilder ? s.attr('data-simulate-touch') === "true" : false,
					onTransitionStart:        function (swiper) {
						toggleSwiperInnerVideos(swiper);
					},
					onTransitionEnd:          function (swiper) {
						toggleSwiperCaptionAnimation(swiper);
					},
					onInit:                   (function (s) {
						return function (swiper) {
							toggleSwiperInnerVideos(swiper);
							toggleSwiperCaptionAnimation(swiper);

							var $swiper = $(s);

							var swiperCustomIndex = $swiper.find('.swiper-pagination__fraction-index').get(0),
									swiperCustomCount = $swiper.find('.swiper-pagination__fraction-count').get(0);

							if (swiperCustomIndex && swiperCustomCount) {
								swiperCustomIndex.innerHTML = formatIndex(swiper.realIndex + 1);
								if (swiperCustomCount) {
									if (isNoviBuilder ? false : s.attr('data-loop') !== "false") {
										swiperCustomCount.innerHTML = formatIndex(swiper.slides.length - 2);
									} else {
										swiperCustomCount.innerHTML = formatIndex(swiper.slides.length);
									}
								}
							}
						}
					}(s)),
					onSlideChangeStart:       (function (s) {
						return function (swiper) {
							var swiperCustomIndex = $(s).find('.swiper-pagination__fraction-index').get(0);

							if (swiperCustomIndex) {
								swiperCustomIndex.innerHTML = formatIndex(swiper.realIndex + 1);
							}
						}
					}(s))
				});

				$window.on("resize", (function (s) {
					return function () {
						var mh = getSwiperHeight(s, "min-height"),
								h = getSwiperHeight(s, "height");
						if (h) {
							s.css("height", mh ? mh > h ? mh : h : h);
						}
					}
				})(s)).trigger("resize");
			}
		}

		function formatIndex(index) {
			return index < 10 ? '0' + index : index;
		}


// MailChimp Ajax subscription
if (plugins.mailchimp.length) {
	for (i = 0; i < plugins.mailchimp.length; i++) {
		var $mailchimpItem = $(plugins.mailchimp[i]),
				$email = $mailchimpItem.find('input[type="email"]');

		// Required by MailChimp
		$mailchimpItem.attr('novalidate', 'true');
		$email.attr('name', 'EMAIL');

		$mailchimpItem.on('submit', $.proxy(function ($email, event) {
			event.preventDefault();

			var $this = this;

			var data = {},
					url = $this.attr('action').replace('/post?', '/post-json?').concat('&c=?'),
					dataArray = $this.serializeArray(),
					$output = $("#" + $this.attr("data-form-output"));

			for (i = 0; i < dataArray.length; i++) {
				data[dataArray[i].name] = dataArray[i].value;
			}

			$.ajax({
				data:       data,
				url:        url,
				dataType:   'jsonp',
				error:      function (resp, text) {
					$output.html('Server error: ' + text);

					setTimeout(function () {
						$output.removeClass("active");
					}, 4000);
				},
				success:    function (resp) {
					$output.html(resp.msg).addClass('active');
					$email[0].value = '';
					var $label = $('[for="' + $email.attr('id') + '"]');
					if ($label.length) $label.removeClass('focus not-empty');

					setTimeout(function () {
						$output.removeClass("active");
					}, 6000);
				},
				beforeSend: function (data) {
					var isNoviBuilder = window.xMode;

					var isValidated = (function () {
						var results, errors = 0;
						var elements = $this.find('[data-constraints]');
						var captcha = null;
						if (elements.length) {
							for (var j = 0; j < elements.length; j++) {

								var $input = $(elements[j]);
								if ((results = $input.regula('validate')).length) {
									for (var k = 0; k < results.length; k++) {
										errors++;
										$input.siblings(".form-validation").text(results[k].message).parent().addClass("has-error");
									}
								} else {
									$input.siblings(".form-validation").text("").parent().removeClass("has-error")
								}
							}

							if (captcha) {
								if (captcha.length) {
									return validateReCaptcha(captcha) && errors === 0
								}
							}

							return errors === 0;
						}
						return true;
					})();

					// Stop request if builder or inputs are invalide
					if (isNoviBuilder || !isValidated)
						return false;

					$output.html('Submitting...').addClass('active');
				}
			});

			return false;
		}, $mailchimpItem, $email));
	}
}

// Campaign Monitor ajax subscription
if (plugins.campaignMonitor.length) {
	for (i = 0; i < plugins.campaignMonitor.length; i++) {
		var $campaignItem = $(plugins.campaignMonitor[i]);

		$campaignItem.on('submit', $.proxy(function (e) {
			var data = {},
					url = this.attr('action'),
					dataArray = this.serializeArray(),
					$output = $("#" + plugins.campaignMonitor.attr("data-form-output")),
					$this = $(this);

			for (i = 0; i < dataArray.length; i++) {
				data[dataArray[i].name] = dataArray[i].value;
			}

			$.ajax({
				data:       data,
				url:        url,
				dataType:   'jsonp',
				error:      function (resp, text) {
					$output.html('Server error: ' + text);

					setTimeout(function () {
						$output.removeClass("active");
					}, 4000);
				},
				success:    function (resp) {
					$output.html(resp.Message).addClass('active');

					setTimeout(function () {
						$output.removeClass("active");
					}, 6000);
				},
				beforeSend: function (data) {
					// Stop request if builder or inputs are invalide
					if (isNoviBuilder || !isValidated($this.find('[data-constraints]')))
						return false;

					$output.html('Submitting...').addClass('active');
				}
			});

			// Clear inputs after submit
			var inputs = $this[0].getElementsByTagName('input');
			for (var i = 0; i < inputs.length; i++) {
				inputs[i].value = '';
				var label = document.querySelector('[for="' + inputs[i].getAttribute('id') + '"]');
				if (label) label.classList.remove('focus', 'not-empty');
			}

			return false;
		}, $campaignItem));
	}
}

// RD Mailform
if (plugins.rdMailForm.length) {
	var i, j, k,
			msg = {
				'MF000': 'Successfully sent!',
				'MF001': 'Recipients are not set!',
				'MF002': 'Form will not work locally!',
				'MF003': 'Please, define email field in your form!',
				'MF004': 'Please, define type of your form!',
				'MF254': 'Something went wrong with PHPMailer!',
				'MF255': 'Aw, snap! Something went wrong.'
			};

	for (i = 0; i < plugins.rdMailForm.length; i++) {
		var $form = $(plugins.rdMailForm[i]),
				formHasCaptcha = false;

		$form.attr('novalidate', 'novalidate').ajaxForm({
			data:         {
				"form-type": $form.attr("data-form-type") || "contact",
				"counter":   i
			},
			beforeSubmit: function (arr, $form, options) {
				if (isNoviBuilder)
					return;

				var form = $(plugins.rdMailForm[this.extraData.counter]),
						inputs = form.find("[data-constraints]"),
						output = $("#" + form.attr("data-form-output")),
						captcha = form.find('.recaptcha'),
						captchaFlag = true;

				output.removeClass("active error success");

				if (isValidated(inputs, captcha)) {

					// veify reCaptcha
					if (captcha.length) {
						var captchaToken = captcha.find('.g-recaptcha-response').val(),
								captchaMsg = {
									'CPT001': 'Please, setup you "site key" and "secret key" of reCaptcha',
									'CPT002': 'Something wrong with google reCaptcha'
								};

						formHasCaptcha = true;

						$.ajax({
							method: "POST",
							url:    "bat/reCaptcha.php",
							data:   {'g-recaptcha-response': captchaToken},
							async:  false
						})
						.done(function (responceCode) {
							if (responceCode !== 'CPT000') {
								if (output.hasClass("snackbars")) {
									output.html('<p><span class="icon text-middle mdi mdi-check icon-xxs"></span><span>' + captchaMsg[responceCode] + '</span></p>')

									setTimeout(function () {
										output.removeClass("active");
									}, 3500);

									captchaFlag = false;
								} else {
									output.html(captchaMsg[responceCode]);
								}

								output.addClass("active");
							}
						});
					}

					if (!captchaFlag) {
						return false;
					}

					form.addClass('form-in-process');

					if (output.hasClass("snackbars")) {
						output.html('<p><span class="icon text-middle fa fa-circle-o-notch fa-spin icon-xxs"></span><span>Sending</span></p>');
						output.addClass("active");
					}
				} else {
					return false;
				}
			},
			error:        function (result) {
				if (isNoviBuilder)
					return;

				var output = $("#" + $(plugins.rdMailForm[this.extraData.counter]).attr("data-form-output")),
						form = $(plugins.rdMailForm[this.extraData.counter]);

				output.text(msg[result]);
				form.removeClass('form-in-process');

				if (formHasCaptcha) {
					grecaptcha.reset();
				}
			},
			success:      function (result) {
				if (isNoviBuilder)
					return;

				var form = $(plugins.rdMailForm[this.extraData.counter]),
						output = $("#" + form.attr("data-form-output")),
						select = form.find('select');

				form
				.addClass('success')
				.removeClass('form-in-process');

				if (formHasCaptcha) {
					grecaptcha.reset();
				}

				result = result.length === 5 ? result : 'MF255';
				output.text(msg[result]);

				if (result === "MF000") {
					if (output.hasClass("snackbars")) {
						output.html('<p><span class="icon text-middle mdi mdi-check icon-xxs"></span><span>' + msg[result] + '</span></p>');
					} else {
						output.addClass("active success");
					}
				} else {
					if (output.hasClass("snackbars")) {
						output.html(' <p class="snackbars-left"><span class="icon icon-xxs mdi mdi-alert-outline text-middle"></span><span>' + msg[result] + '</span></p>');
					} else {
						output.addClass("active error");
					}
				}

				form.clearForm();

				if (select.length) {
					select.select2("val", "");
				}

				form.find('input, textarea').trigger('blur');

				setTimeout(function () {
					output.removeClass("active error success");
					form.removeClass('success');
				}, 3500);
			}
		})
	}
};
