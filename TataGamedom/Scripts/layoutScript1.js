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
