// 收合 SideNav
var toggle = document.querySelector('#toggle');
var logo = document.querySelector('#logo');

function toggleSideNav() {
  var parent = toggle.parentNode;
  parent.classList.toggle('width');

  var children = toggle.children;
  for (var i = 0; i < children.length; i++) {
    children[i].classList.toggle('fa-chevron-circle-left');
    children[i].classList.toggle('fa-chevron-circle-right');
  }
}
toggle.addEventListener('click', toggleSideNav);
logo.addEventListener('click', toggleSideNav);

// 上方時間
var timeSpan = document.querySelector('#timeSpan');

function updateDateTime() {
  var currentDate = new Date();
  var options = {
    year: 'numeric',
    month: '2-digit',
    day: '2-digit',
    hour: '2-digit',
    minute: '2-digit',
    second: '2-digit',
    hour12: false,
  };
  var formattedDateTime = currentDate.toLocaleString('ja-JP');
  timeSpan.textContent = formattedDateTime;
}

updateDateTime();
setInterval(updateDateTime, 1000);


////tiny edit
//tinymce.init({
//	selector: 'textarea',
//	plugins: '',
//	toolbar: 'blocks fontfamily fontsize | bold italic underline strikethrough |  align lineheight | checklist numlist bullist indent outdent |',
//	tinycomments_mode: 'embedded',
//	tinycomments_author: 'Author name',
//	mergetags_list: [
//		{ value: 'First.Name', title: 'First Name' },
//		{ value: 'Email', title: 'Email' },
//	]
//});

//close window => logout
// 在視窗關閉時觸發登出
//window.addEventListener('beforeunload', function () {
//    // 執行登出的相關程式碼，例如發送登出請求
//    logout();
//});

//function logout() {
//    document.cookie = "membershipdemo=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
//}