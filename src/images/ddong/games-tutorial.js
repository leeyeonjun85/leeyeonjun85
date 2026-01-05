
let canvas = document.getElementById('tutorial');


function draw1() {
    if (canvas.getContext) {
        let ctx = canvas.getContext('2d');
    
        ctx.fillStyle = "rgb(200,0,0)";
        ctx.fillRect (10, 10, 50, 50);

        ctx.fillStyle = "rgba(0, 0, 200, 0.5)";
        ctx.fillRect (30, 30, 50, 50);
    }
}

function draw2() {
    if (canvas.getContext) {
        let ctx = canvas.getContext('2d');
    
        ctx.fillRect(25, 25, 100, 100);
        ctx.clearRect(45, 45, 60, 60);
        ctx.strokeRect(50, 50, 50, 50);
    }
}

function draw3() {
    if (canvas.getContext) {
        let ctx = canvas.getContext('2d');
    
        ctx.beginPath();
        ctx.moveTo(75, 50);
        ctx.lineTo(100, 75);
        ctx.lineTo(100, 25);
        ctx.fill();
    }
}

function draw4() {
    if (canvas.getContext) {
        let ctx = canvas.getContext('2d');
    
        ctx.beginPath();
        ctx.arc(75, 75, 50, 0, Math.PI * 2, true); // Outer circle
        ctx.moveTo(110, 75);
        ctx.arc(75, 75, 35, 0, Math.PI, false);  // Mouth (clockwise)
        ctx.moveTo(65, 65);
        ctx.arc(60, 65, 5, 0, Math.PI * 2, true);  // Left eye
        ctx.moveTo(95, 65);
        ctx.arc(90, 65, 5, 0, Math.PI * 2, true);  // Right eye
        ctx.stroke();
    }
}

function draw5() {
    if (canvas.getContext) {
        let ctx = canvas.getContext('2d');
    
        // Filled triangle
        ctx.beginPath();
        ctx.moveTo(25, 25);
        ctx.lineTo(105, 25);
        ctx.lineTo(25, 105);
        ctx.fill();

        // Stroked triangle
        ctx.beginPath();
        ctx.moveTo(125, 125);
        ctx.lineTo(125, 45);
        ctx.lineTo(45, 125);
        ctx.closePath();
        ctx.stroke();
    }
}

function draw6() {
    if (canvas.getContext) {
        let ctx = canvas.getContext('2d');
    
        for (var i = 0; i < 4; i++) {
            for (var j = 0; j < 3; j++) {
                ctx.beginPath();
                var x = 25 + j * 50; // x coordinate
                var y = 25 + i * 50; // y coordinate
                var radius = 20; // Arc radius
                var startAngle = 0; // Starting point on circle
                var endAngle = Math.PI + (Math.PI * j) / 2; // End point on circle
                var anticlockwise = i % 2 == 0 ? false : true; // clockwise or anticlockwise
        
                ctx.arc(x, y, radius, startAngle, endAngle, anticlockwise);
        
                if (i > 1) {
                ctx.fill();
                } else {
                ctx.stroke();
                }
            }
        }
    }
}


function draw7() {
    if (canvas.getContext) {
        let ctx = canvas.getContext('2d');
    
        // Quadratric curves example
        ctx.beginPath();
        ctx.moveTo(75, 25);
        ctx.quadraticCurveTo(25, 25, 25, 62.5);
        ctx.quadraticCurveTo(25, 100, 50, 100);
        ctx.quadraticCurveTo(50, 120, 30, 125);
        ctx.quadraticCurveTo(60, 120, 65, 100);
        ctx.quadraticCurveTo(125, 100, 125, 62.5);
        ctx.quadraticCurveTo(125, 25, 75, 25);
        ctx.stroke();

    }
}

function draw8() {
    if (canvas.getContext) {
        let ctx = canvas.getContext('2d');
    
        // Cubic curves example
        ctx.beginPath();
        ctx.moveTo(75, 40);
        ctx.bezierCurveTo(75, 37, 70, 25, 50, 25);
        ctx.bezierCurveTo(20, 25, 20, 62.5, 20, 62.5);
        ctx.bezierCurveTo(20, 80, 40, 102, 75, 120);
        ctx.bezierCurveTo(110, 102, 130, 80, 130, 62.5);
        ctx.bezierCurveTo(130, 62.5, 130, 25, 100, 25);
        ctx.bezierCurveTo(85, 25, 75, 37, 75, 40);
        ctx.fill();

    }
}


function draw9() {
    if (canvas.getContext) {
        let ctx = canvas.getContext('2d');
  
        roundedRect(ctx, 12, 12, 150, 150, 15);
        roundedRect(ctx, 19, 19, 150, 150, 9);
        roundedRect(ctx, 53, 53, 49, 33, 10);
        roundedRect(ctx, 53, 119, 49, 16, 6);
        roundedRect(ctx, 135, 53, 49, 33, 10);
        roundedRect(ctx, 135, 119, 25, 49, 10);

        ctx.beginPath();
        ctx.arc(37, 37, 13, Math.PI / 7, -Math.PI / 7, false);
        ctx.lineTo(31, 37);
        ctx.fill();

        for (var i = 0; i < 8; i++) {
        ctx.fillRect(51 + i * 16, 35, 4, 4);
        }

        for (i = 0; i < 6; i++) {
        ctx.fillRect(115, 51 + i * 16, 4, 4);
        }

        for (i = 0; i < 8; i++) {
        ctx.fillRect(51 + i * 16, 99, 4, 4);
        }

        ctx.beginPath();
        ctx.moveTo(83, 116);
        ctx.lineTo(83, 102);
        ctx.bezierCurveTo(83, 94, 89, 88, 97, 88);
        ctx.bezierCurveTo(105, 88, 111, 94, 111, 102);
        ctx.lineTo(111, 116);
        ctx.lineTo(106.333, 111.333);
        ctx.lineTo(101.666, 116);
        ctx.lineTo(97, 111.333);
        ctx.lineTo(92.333, 116);
        ctx.lineTo(87.666, 111.333);
        ctx.lineTo(83, 116);
        ctx.fill();

        ctx.fillStyle = 'white';
        ctx.beginPath();
        ctx.moveTo(91, 96);
        ctx.bezierCurveTo(88, 96, 87, 99, 87, 101);
        ctx.bezierCurveTo(87, 103, 88, 106, 91, 106);
        ctx.bezierCurveTo(94, 106, 95, 103, 95, 101);
        ctx.bezierCurveTo(95, 99, 94, 96, 91, 96);
        ctx.moveTo(103, 96);
        ctx.bezierCurveTo(100, 96, 99, 99, 99, 101);
        ctx.bezierCurveTo(99, 103, 100, 106, 103, 106);
        ctx.bezierCurveTo(106, 106, 107, 103, 107, 101);
        ctx.bezierCurveTo(107, 99, 106, 96, 103, 96);
        ctx.fill();

        ctx.fillStyle = 'black';
        ctx.beginPath();
        ctx.arc(101, 102, 2, 0, Math.PI * 2, true);
        ctx.fill();

        ctx.beginPath();
        ctx.arc(89, 102, 2, 0, Math.PI * 2, true);
        ctx.fill();
    }
}
  
// A utility function to draw a rectangle with rounded corners.
function roundedRect(ctx, x, y, width, height, radius) {
    ctx.beginPath();
    ctx.moveTo(x, y + radius);
    ctx.lineTo(x, y + height - radius);
    ctx.arcTo(x, y + height, x + radius, y + height, radius);
    ctx.lineTo(x + width - radius, y + height);
    ctx.arcTo(x + width, y + height, x + width, y + height-radius, radius);
    ctx.lineTo(x + width, y + radius);
    ctx.arcTo(x + width, y, x + width - radius, y, radius);
    ctx.lineTo(x + radius, y);
    ctx.arcTo(x, y, x, y + radius, radius);
    ctx.stroke();
}


function draw10() {
    if (canvas.getContext) {
        let ctx = canvas.getContext('2d');
  
        let rectangle = new Path2D();
        rectangle.rect(10, 10, 50, 50);

        let circle = new Path2D();
        circle.moveTo(125, 35);
        circle.arc(100, 35, 25, 0, 2 * Math.PI);

        let q = new Path2D('M110 100 h 80 v 80 h -80 Z');
        let p = new Path2D('M10 100 h 80 v 80 h -80 Z');

        ctx.stroke(rectangle);
        ctx.fill(circle);
        ctx.stroke(q);
        ctx.fill(p);

    }
}

function draw11() {
    if (canvas.getContext) {
        let ctx = canvas.getContext('2d');
  
        for (var i = 0; i < 6; i++){
            for (var j = 0; j < 6; j++){
                ctx.fillStyle = 'rgb(' + Math.floor(255 - 42.5 * i) + ', ' +
                                Math.floor(255 - 42.5 * j) + ', 150, 0.5)'; // 맨 뒤에는 투명도
                ctx.fillRect(j*25,i*25,25,25);
            }
        }

    }
}

function draw12() {
    if (canvas.getContext) {
        let ctx = canvas.getContext('2d');
  
        for (var i = 0; i < 6; i++) {
            for (var j = 0; j < 6; j++) {
              ctx.strokeStyle = 'rgb(0, ' + Math.floor(255 - 42.5 * i) + ', ' +
                               Math.floor(255 - 42.5 * j) + ')';
              ctx.beginPath();
              ctx.arc(12.5 + j * 25, 12.5 + i * 25, 10, 0, Math.PI * 2, true);
              ctx.stroke();
            }
        }

    }
}

function draw13() {
    if (canvas.getContext) {
        let ctx = canvas.getContext('2d');
  
        // 배경을 그린다
        ctx.fillStyle = '#FD0';
        ctx.fillRect(0, 0, 75, 75);
        ctx.fillStyle = '#6C0';
        ctx.fillRect(75, 0, 75, 75);
        ctx.fillStyle = '#09F';
        ctx.fillRect(0, 75, 75, 75);
        ctx.fillStyle = '#F30';
        ctx.fillRect(75, 75, 75, 75);
        ctx.fillStyle = '#FFF';

        // 투명값을 설정한다
        ctx.globalAlpha = 0.2;

        // 반투명한 원을 그린다
        for (var i = 0; i < 7; i++){
            ctx.beginPath();
            ctx.arc(75, 75, 10 + 10 * i, 0, Math.PI * 2, true);
            ctx.fill();
        }

    }
}

function draw14() {
    if (canvas.getContext) {
        let ctx = canvas.getContext('2d');
  
        // 배경을 그린다
        ctx.fillStyle = 'rgb(255,221,0)';
        ctx.fillRect(0,0,150,37.5);
        ctx.fillStyle = 'rgb(102,204,0)';
        ctx.fillRect(0,37.5,150,37.5);
        ctx.fillStyle = 'rgb(0,153,255)';
        ctx.fillRect(0,75,150,37.5);
        ctx.fillStyle = 'rgb(255,51,0)';
        ctx.fillRect(0,112.5,150,37.5);

        // 반투명한 사각형을 그린다
        for (var i=0;i<10;i++){
            ctx.fillStyle = 'rgba(255,255,255,'+(i+1)/10+')';
            for (var j=0;j<4;j++){
            ctx.fillRect(5+i*14,5+j*37.5,14,27.5)
            }
        }

    }
}

function draw15() {
    if (canvas.getContext) {
        let ctx = canvas.getContext('2d');
  
        for (var i = 0; i < 10; i++){
            ctx.lineWidth = 1 + i;
            ctx.beginPath();
            ctx.moveTo(5 + i * 14, 5);
            ctx.lineTo(5 + i * 14, 140);
            ctx.stroke();
        }

    }
}

function draw16() {
    if (canvas.getContext) {
        let ctx = canvas.getContext('2d');
  
        let lineCap = ['butt','round','square'];
        // 안내선을 그린다
        ctx.strokeStyle = '#09f';
        ctx.beginPath();
        ctx.moveTo(10, 10);
        ctx.lineTo(140, 10);
        ctx.moveTo(10, 140);
        ctx.lineTo(140, 140);
        ctx.stroke();

        // 선을 그린다
        ctx.strokeStyle = 'black';
        for (var i=0;i<lineCap.length;i++){
            ctx.lineWidth = 15;
            ctx.lineCap = lineCap[i];
            ctx.beginPath();
            ctx.moveTo(25 + i * 50, 10);
            ctx.lineTo(25 + i * 50,140);
            ctx.stroke();
        }

    }
}

function draw17() {
    if (canvas.getContext) {
        let ctx = canvas.getContext('2d');
  
        let lineJoin = ['round', 'bevel', 'miter'];
        ctx.lineWidth = 10;
        for (var i=0;i<lineJoin.length;i++){
            ctx.lineJoin = lineJoin[i];
            ctx.beginPath();
            ctx.moveTo(-5, 5 + i * 40);
            ctx.lineTo(35, 45 + i * 40);
            ctx.lineTo(75, 5 + i * 40);
            ctx.lineTo(115, 45 + i * 40);
            ctx.lineTo(155, 5 + i * 40);
            ctx.stroke();
        }

    }
}

function draw18() {
    if (canvas.getContext) {
        let ctx = canvas.getContext('2d');
        
        // 입력폼을 그린다
        let miterLimit = document.getElementById('miterLimit');
        if (!miterLimit) {
            let tutorial = document.querySelector('.tutorial')
            let form = document.createElement('form');
            form.setAttribute('onsubmit' , 'return draw()');
            tutorial.appendChild(form);

            let label = document.createElement('label');
            label.innerText = "Miter limit";
            form.appendChild(label);

            let input1 = document.createElement('input');
            input1.setAttribute('type' , 'text');
            input1.setAttribute('size' , '3');
            input1.setAttribute('id' , 'miterLimit');
            input1.setAttribute('value' , 5);
            form.appendChild(input1);

            let input2 = document.createElement('input');
            input2.setAttribute('type' , 'submit');
            input2.setAttribute('value' , 'Redraw');
            form.appendChild(input2);
        }

        // 캔버스를 비운다
        ctx.clearRect(0,0,150,150);

        // 안내선을 그린다
        ctx.strokeStyle = '#09f';
        ctx.lineWidth   = 2;
        ctx.strokeRect(-5,50,160,50);

        // 선 스타일을 설정한다
        ctx.strokeStyle = '#000';
        ctx.lineWidth = 10;

        // 입력 값을 검사한다
        if (document.getElementById('miterLimit').value.match(/\d+(\.\d+)?/)) {
            ctx.miterLimit = parseFloat(document.getElementById('miterLimit').value);
        } else {
            alert('Value must be a positive number');
        }

        // 선을 그린다
        ctx.beginPath();
        ctx.moveTo(0,100);
        for (i=0;i<24;i++){
            var dy = i%2==0 ? 25 : -25 ;
            ctx.lineTo(Math.pow(i,1.5)*2,75+dy);
        }
        ctx.stroke();
        return false;

    }
}


let offset = 0;

function draw19() {
    if (canvas.getContext) {
        let ctx = canvas.getContext('2d');

        // March 버튼을 만든다
        let march = document.getElementById('march');
        if (!march) {
            let tutorial = document.querySelector('.tutorial')
            let march = document.createElement('button');
            march.setAttribute('onclick' , 'march()');
            march.setAttribute('id' , 'march');
            march.innerText = "March";
            tutorial.appendChild(march);
        }

        // 사각형을 그린다
        ctx.clearRect(0, 0, canvas.width, canvas.height);
        ctx.setLineDash([12, 6]);
        ctx.lineDashOffset = -offset;
        ctx.strokeRect(20, 20, 100, 100);
        
    }
}

function march() {
    offset++;
    if (offset > 999) {
      offset = 0;
    }
    draw();
    console.log(offset)
    setTimeout(march, 50);
}


function draw20() {
    if (canvas.getContext) {
        let ctx = canvas.getContext('2d');

        // 그레이디언트를 생성한다
        var lingrad = ctx.createLinearGradient(0, 0, 0, 150);
        lingrad.addColorStop(0, '#00ABEB');
        lingrad.addColorStop(0.5, '#fff');
        lingrad.addColorStop(0.5, '#26C000');
        lingrad.addColorStop(1, '#fff');

        var lingrad2 = ctx.createLinearGradient(0, 50, 0, 95);
        lingrad2.addColorStop(0.5, '#000');
        lingrad2.addColorStop(1, 'rgba(0, 0, 0, 0)');

        // 외곽선과 채움 스타일에 그레이디언트를 적용한다
        ctx.fillStyle = lingrad;
        ctx.strokeStyle = lingrad2;

        // 도형을 그린다
        ctx.fillRect(10, 10, 130, 130);
        ctx.strokeRect(50, 50, 50, 50);
        
    }
}


function draw21() {
    if (canvas.getContext) {
        let ctx = canvas.getContext('2d');

        // 그라디언트를 생성한다
        var radgrad = ctx.createRadialGradient(45,45,10,52,50,30);
        radgrad.addColorStop(0, '#A7D30C');
        radgrad.addColorStop(0.9, '#019F62');
        radgrad.addColorStop(1, 'rgba(1,159,98,0)');

        var radgrad2 = ctx.createRadialGradient(105,105,20,112,120,50);
        radgrad2.addColorStop(0, '#FF5F98');
        radgrad2.addColorStop(0.75, '#FF0188');
        radgrad2.addColorStop(1, 'rgba(255,1,136,0)');

        var radgrad3 = ctx.createRadialGradient(95,15,15,102,20,40);
        radgrad3.addColorStop(0, '#00C9FF');
        radgrad3.addColorStop(0.8, '#00B5E2');
        radgrad3.addColorStop(1, 'rgba(0,201,255,0)');

        var radgrad4 = ctx.createRadialGradient(0,150,50,0,140,90);
        radgrad4.addColorStop(0, '#F4F201');
        radgrad4.addColorStop(0.8, '#E4C700');
        radgrad4.addColorStop(1, 'rgba(228,199,0,0)');

        var radgrad5 = ctx.createRadialGradient(160,20,50,150,40,90);
        radgrad5.addColorStop(0, '#b0F201');
        radgrad5.addColorStop(0.7, '#b4C700');
        radgrad5.addColorStop(1, 'rgba(1,159,98,0)');

        // 도형을 그린다
        ctx.fillStyle = radgrad5;
        ctx.fillRect(0,0,200,200);
        ctx.fillStyle = radgrad4;
        ctx.fillRect(0,0,200,200);
        ctx.fillStyle = radgrad3;
        ctx.fillRect(0,0,200,200);
        ctx.fillStyle = radgrad2;
        ctx.fillRect(0,0,200,200);
        ctx.fillStyle = radgrad;
        ctx.fillRect(0,0,200,200);
        
    }
}


function draw22() {
    if (canvas.getContext) {
        let ctx = canvas.getContext('2d');

        // 패턴으로 사용할 이미지 오브젝트를 생성한다
        var img = new Image();
        img.src = 'https://lh3.googleusercontent.com/fife/AAbDypDb_LWVKneaNTmdtvHlkiJkhHeV34bnVZSjHe9pyogIdwqiL2_LYz3oj8kt0Y4pxeckLVygL6gNVr-255teRW0kykXwt2dL0RG6r_b_mdoi9sKx0uBslubXpRuKLkqPNLS1WGEj7Psm0W2CFG46JKwg380lgmfk1iL0IeK6zdzSielPOK8FAeQCNy8IyU_tmDx48pT-GXjsJvnvlC0FHaFR7Zjl04YLqYjEcvVXPpvdq8HWaoFED8OZF4YNvRrxWKtSlSRl27hUSMdtDyTlrkVfnevcqNRo3424YstROVqaTgrhzSF80u86lCz1HCzSCrVcy4TxAOe3XbSKpV8kuhhJrBzjboSKyNFEYVS0WasQ3dIwVoioVha6oInOLVek_IGL3-mxzUH-1HNMZMxMM_up34ujItXgUmYyy-nYYDip6vuJ_N85_PoeMMgp5jK1CB4tIf3U9lQhHHdiQAh57vvKuRCkaSnx2R3PwvZivmo4bogFFo2AmatUuRvVk_eraaekUQjPu_WB851Ey_z9McEuf90lLnbiyzvvK8kaHgvV7yZ5XUtxdU32VAO2IrWjKpJpVyaaUjlLIlA3QhwnZ40UFfIbnDCVjDoNJL0wsbu-j_MsqamfdSh0MOd2ywp6VdPk4XQ8J6AI1ZNnXu_p1PCbnXCYv3hF0kA1YjKojpo1m7LA8TRWtVfIpM5iPbWiKPVR74wcT6aIDjHuXyemxfJcqkWyMzDmmQr8OqMN0LPvu4MzEb0q11vQqSUch3HqVHQ8UZtb8Iy-Od7RnZYPsxcG_So6ZYh5bo_RZqU2HDlh5W1N0-chZ2x9XlEQ-5cpolJBSppI3USzsf2WFpiizUZv5N0bVLSdOtHx0UXJSv-r4uXuYHPHobE1i1_9fUHMVWZVcdnO2hJTI08sr73CGy9ckfzMvXtgV7GKvFQZbQSMJ2v5pGrCMCjQsYBdk0eXzzVMfd-hpXgVcwmXHjbFjO5XLX0ggs69Q-bduzU7mD7U5r69n_mHFcRVLxzA1kPH7zeKF_oU5tclRNafD8lXudib-2bLzlMskTO3RKJ6W76cAu_o2HS4EZA_DN2SgC4DMAINj1yZexgYfku2i1Fx1jyPWWBrGqUXTlhfzptrXFCTQ51QKym-Se_im7LaeDuZqKNQ1jung2WqJR6WeuUrpLjijsrxeBjHxU5SGCpr7-QrPaqYeggu5jlgz-WzWvrLgCM38J_oo-aggou7cZHn7S0LSHaWtsxE8F4xucztxAFE_KuslQv5XrawVqotRfrcDRwE8tT-ksJ-nSfbxozfhBw7SoHhBco_oENowZmiFMeh1JD9yZRFlqEXUZAUmdOA8NkxTEHEpCYFs6BnetHHjPLL1WiiNZUE46hQqM6JpVTE5qT2ukPVZnvQ7zdgHGoXV199kqE_SSZTZP-anasOMktK7u50jvSVLqb9vikJaJIk_6L9RH6x-0NuKcU2_HlK-ibnVqtdJvcS-Y-HtRU-C9D9z49lsG52nu17QzMC8b0IVMe7hlcoGk4iQkc0efeA1SskcF-KJxIoppNLpeTc3t3zxpefq3eQVJUUPeFt9aJecfgsMneo2eoBKIY7VkYQ=w1920-h865';
        
        img.onload = function() {

        // 패턴을 생성한다
        var ptrn = ctx.createPattern(img,'repeat');
        ctx.fillStyle = ptrn;
        ctx.fillRect(0,0,300,300);
        }
        
    }
}

function draw23() {
    if (canvas.getContext) {
        let ctx = canvas.getContext('2d');

        ctx.shadowOffsetX = 2;
        ctx.shadowOffsetY = 2;
        ctx.shadowBlur = 2;
        ctx.shadowColor = "rgba(0, 0, 0, 0.5)";

        ctx.font = "20px Times New Roman";
        ctx.fillStyle = "Black";
        ctx.fillText("Sample String", 5, 30);
        
    }
}

function draw24() {
    if (canvas.getContext) {
        let ctx = canvas.getContext('2d');

        ctx.beginPath();
        ctx.arc(50, 50, 30, 0, Math.PI * 2, true);
        ctx.arc(50, 50, 15, 0, Math.PI * 2, true);
        ctx.fill('evenodd');
        
    }
}

function draw25() {
    if (canvas.getContext) {
        let ctx = canvas.getContext('2d');

        ctx.font = '48px serif';
        ctx.strokeText('Hello', 10, 50);
        ctx.fillText('Hello', 10, 100);

        ctx.textAlign = 'center';
        ctx.fillText('Hello', 10, 150);

    }
}

function draw26() {
    if (canvas.getContext) {
        let ctx = canvas.getContext('2d');

        ctx.fillRect(0, 0, 150, 150);   // 기본 설정으로 사각형을 그리기
        ctx.save();                  // 기본 상태를 저장하기

        ctx.fillStyle = '#09F';      // 설정 변경하기
        ctx.fillRect(15, 15, 120, 120); // 새로운 설정으로 사각형 그리기
        ctx.save();                  // 현재 상태 저장하기

        ctx.fillStyle = '#FFF';      // 설정 변경하기
        ctx.globalAlpha = 0.5;
        ctx.fillRect(30, 30, 90, 90);   // 새로운 설정으로 사각형 그리기

        ctx.restore();               // 이전 상태 복원하기
        ctx.fillRect(45, 45, 60, 60);   // 복원된 설정으로 사각형 그리기

        ctx.restore();               // 초기 상태를 복원하기
        ctx.fillRect(60, 60, 30, 30);   // 복원된 설정으로 사각형 그리기

    }
}

function draw27() {
    if (canvas.getContext) {
        let ctx = canvas.getContext('2d');

        for (var i = 0; i < 3; i++) {
            for (var j = 0; j < 3; j++) {
              ctx.save();
              ctx.fillStyle = 'rgb(' + (51 * i) + ', ' + (255 - 51 * i) + ', 255)';
              ctx.translate(10 + j * 50, 10 + i * 50);
              ctx.fillRect(0, 0, 25, 25);
              ctx.restore();
            }
          }

    }
}

function draw28() {
    if (canvas.getContext) {
        let ctx = canvas.getContext('2d');

        // 좌측 사각형, canvas 원점에서 회전하기
        ctx.save();
        // 파란 사각형
        ctx.fillStyle = '#0095DD';
        ctx.fillRect(30, 30, 100, 100);
        ctx.rotate((Math.PI / 180) * 25);
        // 회색 사각형
        ctx.fillStyle = '#4D4E53';
        ctx.fillRect(30, 30, 100, 100);
        ctx.restore();

        // 우측 사각형, 사각형 중심에서 회전하기
        // 파란 사각형 그리기
        ctx.fillStyle = '#0095DD';
        ctx.fillRect(150, 30, 100, 100);

        ctx.translate(200, 80); // 사각형 중심으로 이동하기
                                // x = x + 0.5 * width
                                // y = y + 0.5 * height
        ctx.rotate((Math.PI / 180) * 25); // 회전
        ctx.translate(-200, -80); // 예전 위치로 이동하기

        // 회색 사각형 그리기
        ctx.fillStyle = '#4D4E53';
        ctx.fillRect(150, 30, 100, 100);

    }
}

function draw29() {
    if (canvas.getContext) {
        let ctx = canvas.getContext('2d');

        // 간단하지만 확대·축소 비율을 적용한 사각형 그리기
        ctx.save();
        ctx.scale(10, 3);
        ctx.fillRect(1, 10, 10, 10);
        ctx.restore();

        // 수평으로 대칭하기
        ctx.scale(-1, 1);
        ctx.font = '48px serif';
        ctx.fillText('MDN', -150, 120);

    }
}

function draw30() {
    if (canvas.getContext) {
        let ctx = canvas.getContext('2d');

        var sin = Math.sin(Math.PI / 6);
        var cos = Math.cos(Math.PI / 6);
        ctx.translate(100, 100);
        var c = 0;
        for (var i = 0; i <= 12; i++) {
            c = Math.floor(255 / 12 * i);
            ctx.fillStyle = 'rgb(' + c + ', ' + c + ', ' + c + ')';
            ctx.fillRect(0, 0, 100, 10);
            ctx.transform(cos, sin, -sin, cos, 0, 0);
            //a (m11)수평으로 확대·축소하기
            //b (m12)수평으로 비스듬히 기울이기
            //c (m21)수직으로 비스듬히 기울이기
            //d (m22)수직으로 확대·축소하기
            //e (dx)수평으로 이동하기
            //f (dy)수직으로 이동하기
        }

        for (let i = 0; i<3; i++){
            ctx.setTransform(-1, 0, 0, 1, 100+i*80, 100+i*10);
            ctx.fillStyle = 'rgba(255, 128, 255, 0.5)';
            ctx.fillRect(0, 50, 100, 100);
        }

    }
}

function draw31() {
    if (canvas.getContext) {
        let ctx = canvas.getContext('2d');

        ctx.fillRect(0,0,150,150);
        ctx.translate(75,75);

        // 동그란 모양의 잘라내기 경로를 생성한다
        ctx.beginPath();
        ctx.arc(0,0,60,0,Math.PI*2,true);
        ctx.clip();

        // 배경을 그린다
        var lingrad = ctx.createLinearGradient(0,-75,0,75);
        lingrad.addColorStop(0, '#232256');
        lingrad.addColorStop(1, '#143778');

        ctx.fillStyle = lingrad;
        ctx.fillRect(-75,-75,150,150);

        // 별을 그린다
        for (var j=1;j<50;j++){
            ctx.save();
            ctx.fillStyle = '#fff';
            ctx.translate(75-Math.floor(Math.random()*150),
                        75-Math.floor(Math.random()*150));
            drawStar(ctx,Math.floor(Math.random()*4)+2);
            ctx.restore();
        }

    }
}

function drawStar(ctx,r){
    ctx.save();
    ctx.beginPath()
    ctx.moveTo(r,0);
    for (var i=0;i<9;i++){
      ctx.rotate(Math.PI/5);
      if(i%2 == 0) {
        ctx.lineTo((r/0.525731)*0.200811,0);
      } else {
        ctx.lineTo(r,0);
      }
    }
    ctx.closePath();
    ctx.fill();
    ctx.restore();
  }


// ======================================================================================== //
// ======================================================================================== //

function init(){
    clock();
    setInterval(clock,1000);
}
  

function clock() {
    if (canvas.getContext) {
        var now = new Date();
        let ctx = canvas.getContext('2d');
        

        ctx.save();
        ctx.clearRect(0,0,150,150);
        ctx.translate(75,75);
        ctx.scale(0.4,0.4);
        ctx.rotate(-Math.PI/2);
        ctx.strokeStyle = "black";
        ctx.fillStyle = "white";
        ctx.lineWidth = 8;
        ctx.lineCap = "round";
    
        // 시계판 - 시
        ctx.save();
        for (var i=0;i<12;i++){
            ctx.beginPath();
            ctx.rotate(Math.PI/6);
            ctx.moveTo(100,0);
            ctx.lineTo(120,0);
            ctx.stroke();
            }
            ctx.restore();
        
            // 시계판 - 분
            ctx.save();
            ctx.lineWidth = 5;

        for (i=0;i<60;i++){
        if (i%5!=0) {
            ctx.beginPath();
            ctx.moveTo(117,0);
            ctx.lineTo(120,0);
            ctx.stroke();
        }
        ctx.rotate(Math.PI/30);
        }
        ctx.restore();
    
        var sec = now.getSeconds();
        var min = now.getMinutes();
        var hr  = now.getHours();
        hr = hr>=12 ? hr-12 : hr;
    
        ctx.fillStyle = "black";
    
        // 시간 표시 - 시
        ctx.save();
        ctx.rotate( hr*(Math.PI/6) + (Math.PI/360)*min + (Math.PI/21600)*sec )
        ctx.lineWidth = 14;
        ctx.beginPath();
        ctx.moveTo(-20,0);
        ctx.lineTo(80,0);
        ctx.stroke();
        ctx.restore();
    
        // 시간 표시 - 분
        ctx.save();
        ctx.rotate( (Math.PI/30)*min + (Math.PI/1800)*sec )
        ctx.lineWidth = 10;
        ctx.beginPath();
        ctx.moveTo(-28,0);
        ctx.lineTo(112,0);
        ctx.stroke();
        ctx.restore();
    
        // 시간 표시 - 초
        ctx.save();
        ctx.rotate(sec * Math.PI/30);
        ctx.strokeStyle = "#D40000";
        ctx.fillStyle = "#D40000";
        ctx.lineWidth = 6;
        ctx.beginPath();
        ctx.moveTo(-30,0);
        ctx.lineTo(83,0);
        ctx.stroke();
        ctx.beginPath();
        ctx.arc(0,0,10,0,Math.PI*2,true);
        ctx.fill();
        ctx.beginPath();
        ctx.arc(95,0,10,0,Math.PI*2,true);
        ctx.stroke();
        ctx.fillStyle = "rgba(0,0,0,0)";
        ctx.arc(0,0,3,0,Math.PI*2,true);
        ctx.fill();
        ctx.restore();
    
        ctx.beginPath();
        ctx.lineWidth = 14;
        ctx.strokeStyle = '#325FA2';
        ctx.arc(0,0,142,0,Math.PI*2,true);
        ctx.stroke();
    
        ctx.restore();
    }
}


// ======================================================================================== //
// ======================================================================================== //


var img = new Image();

// 변수
// 스크롤될 이미지, 방향, 속도를 바꾸려면 변수값을 바꾼다.

img.src = 'https://www.freewebheaders.com/wp-content/uploads/water-coast-header-800x200.jpg';
var CanvasXSize = 800;
var CanvasYSize = 200;
var speed = 30; // 값이 작을 수록 빨라진다
var scale = 1.05;
var y = -4.5; // 수직 옵셋

// 주요 프로그램

var dx = 0.75;
var imgW;
var imgH;
var x = 0;
var clearX;
var clearY;
var ctx;

img.onload = function() {
    imgW = img.width*scale;
    imgH = img.height*scale;
    if (imgW > CanvasXSize) { x = CanvasXSize-imgW; } // 캔버스보다 큰 이미지
    if (imgW > CanvasXSize) { clearX = imgW; } // 캔버스보다 큰 이미지
    else { clearX = CanvasXSize; }
    if (imgH > CanvasYSize) { clearY = imgH; } // 캔버스보다 큰 이미지
    else { clearY = CanvasYSize; }
    // 캔버스 요소 얻기
    ctx = canvas.getContext('2d');
    // 새로 그리기 속도 설정
    return setInterval(draw, speed);
}

function draw32() {
    // 캔버스를 비운다
    ctx.clearRect(0,0,clearX,clearY);
    // 이미지가 캔버스보다 작거나 같다면 (If image is <= Canvas Size)
    if (imgW <= CanvasXSize) {
        // 재설정, 처음부터 시작
        if (x > (CanvasXSize)) { x = 0; }
        // 추가 이미지 그리기
        if (x > (CanvasXSize-imgW)) { ctx.drawImage(img,x-CanvasXSize+1,y,imgW,imgH); }
    }
    // 이미지가 캔버스보다 크다면 (If image is > Canvas Size)
    else {
        // 재설정, 처음부터 시작
        if (x > (CanvasXSize)) { x = CanvasXSize-imgW; }
        // 추가 이미지 그리기
        if (x > (CanvasXSize-imgW)) { ctx.drawImage(img,x-imgW+1,y,imgW,imgH); }
    }
    // 이미지 그리기
    ctx.drawImage(img,x,y,imgW,imgH);
    // 움직임 정도
    x += dx;
}









var ctx = canvas.getContext('2d');
var raf;
var running = false;

var ball = {
  x: 300,
  y: 80,
  vx: 5,
  vy: 1,
  radius: 25,
  color: 'blue',
  draw: function() {
    ctx.beginPath();
    ctx.arc(this.x, this.y, this.radius, 0, Math.PI * 2, true);
    ctx.closePath();
    ctx.fillStyle = this.color;
    ctx.fill();
  }
};

function clear() {
  ctx.fillStyle = 'rgba(255, 255, 255, 0.3)';
  ctx.fillRect(0,0,canvas.width,canvas.height);
}

function draw() {
  clear();
  ball.draw();
  ball.x += ball.vx;
  ball.y += ball.vy;

  if (ball.y + ball.vy > canvas.height || ball.y + ball.vy < 0) {
    ball.vy = -ball.vy;
  }
  if (ball.x + ball.vx > canvas.width || ball.x + ball.vx < 0) {
    ball.vx = -ball.vx;
  }

  if ( ball.y < canvas.height ) {
    ball.vy *= .99;
    ball.vy += .25;
    ball.vx *= .9953;
  }

  raf = window.requestAnimationFrame(draw);
}

canvas.addEventListener('mousemove', function(e) {
  if (!running) {
    clear();
    ball.x = e.clientX;
    ball.y = e.clientY;
    ball.draw();
  }
});

canvas.addEventListener('click', function(e) {
  if (!running) {
    raf = window.requestAnimationFrame(draw);
    running = true;
  }
});

canvas.addEventListener('mouseout', function(e) {
  window.cancelAnimationFrame(raf);
  running = false;
});

ball.draw();