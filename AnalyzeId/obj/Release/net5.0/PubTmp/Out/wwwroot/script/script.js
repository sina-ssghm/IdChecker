var config;
var canvasHelperText;
var rectWidth = 3;
var stopStreaming = false;
var player = document.getElementById('videoplayer');
var canvas = document.getElementById('streamCanvas');
var userConfig = {
    targetWidth: (window.innerWidth || 950),
    targetHeight: (window.innerHeight),
    frameScale: 1,
    primaryConstraints: {
        video: {
            facingMode: "environment",
            // height: { min: 1440, ideal: 1440 },
            aspectRatio: getAspectRatio()
        }
    }
};
// var helperTexts = {
//     align: 'align',
//     comeCloser: 'Move closer',
//     tapToCapture: 'Tap to capure',
//     keepStill: 'Keep still',
// }
// canvasHelperText = helperTexts.align;

// setTimeout(function () {
//     canvasHelperText = helperTexts.comeCloser;
// }, 6000);

// setTimeout(function () {
//     canvasHelperText = helperTexts.keepStill;
// }, 8000);



function getAspectRatio() {
    if (isIOS()) {
        return 1.33333333
    }
    return Math.max(window.innerWidth, window.innerHeight) * 1.0 / Math.min(window.innerWidth, window.innerHeight)
}






ReadConfigFile()

//check device, token , ... 
function init() {
    if (!checkToken()) {
        document.getElementById('invalidToken').classList.remove('d-none');
        return;
    }
    // PrepareHome()

}

function PrepareHome() {
    document.querySelector('#invalidToken').classList.add('d-none');
    document.querySelector('#scanMenu').classList.add('d-none');
    document.querySelector('#file-uploader-div').classList.add('d-none');
    document.querySelector('.cameraDiv').classList.add('d-none');
    document.querySelector('.container').classList.remove('d-none');

    var isMobile = mobileCheck();

    if (isMobile) {
        var cameraUse = config.mobile.camera;
        var storageUse = config.mobile.storage;
        if (config.mobile.camera == false) {
            document.querySelector('.camera').classList.add('d-none');
        }
        // if (config.mobile.storage == false) {
        //     document.querySelector('.storage').classList.add('d-none');
        // }
        // if (!cameraUse && !storageUse) {
        //     document.getElementById('mobilenoway').classList.remove('d-none');
        // }
    }
    else {
        var cameraUse = config.desktop.camera;
        var storageUse = config.desktop.storage;

        if (!cameraUse) {
            document.querySelector('.camera').classList.add('d-none');
        }
        // if (!storageUse) {
        //     document.querySelector('.storage').classList.add('d-none');
        // }
        // if (!cameraUse && !storageUse) {
        //     document.getElementById('desktopnoway').classList.remove('d-none');

        // }
    }

    document.getElementById('scanMenu').classList.remove('d-none');

}
function readTextFile(file, callback) {
    var rawFile = new XMLHttpRequest();
    rawFile.overrideMimeType("application/json");
    rawFile.open("GET", file, true);
    rawFile.onreadystatechange = function () {
        if (rawFile.readyState === 4 && rawFile.status == "200") {
            callback(rawFile.responseText);
        }
    }
    rawFile.send(null);
}

function ReadConfigFile() {

    readTextFile("/confi.json", function (text) {
        config = JSON.parse(text);
        init();
    });


}


function checkToken(token) {
    return true;
}


function isSafari() {
    //return true;
    var ua = navigator.userAgent.toLowerCase();
    if (ua.indexOf('safari') != -1) {
        if (ua.indexOf('chrome') > -1) {
            return false;
        } else {
            return true;
        }
    }
    return false;
}

function isFireFox() {
    return navigator.userAgent.toLowerCase().indexOf('firefox') > -1;
}
function mobileCheck() {
    var check = false;
    (function (a) { if (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino|android|playbook|silk/i.test(a) || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(a.substr(0, 4))) check = true; })(navigator.userAgent || navigator.vendor || window.opera);
    return (check || isIOS()) && (!isFireFox());
};
function isIOS() {
    return ((/iPad|iPhone|iPod/.test(navigator.platform) && checkIOSVersion()) || (navigator.platform === 'MacIntel' && navigator.maxTouchPoints > 1))
}

function checkIOSVersion() {
    return iOSversion()[0] >= 13;
}

function iOSversion() {
    if (/iP(hone|od|ad)/.test(navigator.platform)) {
        // supports iOS 2.0 and later: <http://bit.ly/TJjs1V>
        var v = (navigator.appVersion).match(/OS (\d+)_(\d+)_?(\d+)?/);
        return [parseInt(v[1], 10), parseInt(v[2], 10), parseInt(v[3] || 0, 10)];
    }
    return ""
}

function getDevice(constraints, errorCallback) {
    navigator.mediaDevices.enumerateDevices()
        .then(function (devices) {
            var min = undefined;
            devices.forEach(function (device) {
                if (device.label && device.label.indexOf("back") !== -1) {
                    let split = device.label.split(',')
                    let type = parseInt(split[0][split[0].length - 1]);

                    if (type || type === 0) {
                        if (min === undefined || min > type) {
                            min = type;
                            constraints.video.deviceId = device.deviceId;
                        }
                    }
                }
            });
            startCamera(constraints, errorCallback);
        })
        .catch(function (err) {
            startCamera(constraints, errorCallback);
        });
}

function fullscreenCanvas() {
    $('.app-content').addClass('content-fullscreen');
}

function exitFullscreenCanvas() {
    $('.app-content').removeClass('content-fullscreen');
}

function startCamera(constraints, errorCallback) {

    navigator.mediaDevices.getUserMedia(constraints)
        .then((stream) => {
            //there is a third party library called screenfully which could enable fullscreen behavior in safari.
            fullscreenCanvas()
            if (isSafari()) {
                enableCamera(stream);
            }
            else {
                requestFullScreen(stream);
            }
        })
        .catch((error) => {
            if (typeof (errorCallback) === "function") {

                errorCallback(error);
            }
        });

}

function requestFullScreen(stream) {
    enableCamera(stream)
    document.querySelector('.camera-video-div')
        // canvas
        .requestFullscreen()
        .then(function () {
            enableCamera(stream)
        })
        .catch(function (error) {
            enableCamera(stream)
        });

}

function exitFullscreen() {

    document

        .exitFullscreen()


}


function enableCamera(stream) {
    // document.querySelector('.container').classList.add('d-none');
    isStarted = true;
    player.srcObject = stream;
    setRepeatFrameProcessor()
    player.play();
    unspin()
}


function initCanvas() {

    canvas.width = videoplayer.videoWidth;
    canvas.height = videoplayer.videoHeight;

}


// document.querySelector('.storage').addEventListener('click', function () {
//     document.querySelector('#scanMenu').classList.add('d-none');
//     document.querySelector('#file-uploader-div').classList.remove('d-none');
//     $('.dropify').dropify();

// });
// document.querySelector('.uploadFileBtn').addEventListener('click', function () {
//     var fileValue = document.querySelector('.dropify').value;
//     if (fileValue != null && fileValue.trim() != '') {

//     }
//     else {
//         alert('No file is selected!')
//     }
// })
// document.querySelector('.homeBtn').addEventListener('click', function () {
//     PrepareHome();
// });

document.querySelector('.camera').addEventListener('click', function () {
    spin();
    document.querySelector('.cameraDiv').classList.remove('d-none');
    document.querySelector('.canvascontainer').classList.add('d-none');
    $('#File').click();
    document.querySelector('#scanMenu').classList.add('d-none');
    //getDevice(userConfig.primaryConstraints, ErrorCallback);
});


document.querySelector('.CanvasCamera').addEventListener('click', function () {
    spin();
    document.querySelector('.cameraDiv').classList.remove('d-none');
    //$('#File').click();
    document.querySelector('.canvascontainer').classList.remove('d-none');

    document.querySelector('#scanMenu').classList.add('d-none');
    getDevice(userConfig.primaryConstraints, ErrorCallback);
});



$('#File').change(function (e) {
    spin();

    var selectedFile = e.target.files[0];
    var reader = new FileReader();

    var imgtag = document.querySelector(".camera-preview-img");


    reader.onload = function (event) {
        imgtag.src = event.target.result;

        $('#camraPreview').removeClass('d-none');
        $('.camera-preview-img').removeClass('d-none');
    };

    unspin();
    reader.readAsDataURL(selectedFile);
});

$('.retryCamera').click(function () {
    $('#File').click();
});

function ErrorCallback(error) {
    document.querySelector('.cameraDiv').classList.add('d-none')
    document.querySelector('.error-alert').classList.remove('d-none');
    document.querySelector('.error-message').innerHTML = error;

    console.error(error);
    unspin()
}


function setRepeatFrameProcessor() {

    drawCanvas();
    // CheckRectangle(whitePaperSetting);
    let delay = 20;
    if (stopStreaming == false) {
        setTimeout(setRepeatFrameProcessor, delay);
    }
}


function drawCanvas() {
    var ctx = canvas.getContext('2d');
    initCanvas();
    ctx.drawImage(videoplayer, 0, 0);

    // DrawBoundingLines();
}

function DrawBoundingLines() {

    var width = canvas.width;
    var height = canvas.height;

    var dh = Math.round(height * 70 / 100);
    var dw = Math.round(dh * 5.3 / 8.5);
    var topleftCorner = {
        x: Math.round((width - dw) / 2),
        y: Math.round((height - dh) / 2)
    };
    var topRightCorner = {
        x: Math.round((width - dw) / 2) + dw,
        y: Math.round((height - dh) / 2)
    };
    var bottomLeftCorner = {
        x: Math.round((width - dw) / 2),
        y: Math.round((height - dh) / 2) + dh
    };
    var bottomRightCorner = {
        x: Math.round((width - dw) / 2) + dw,
        y: Math.round((height - dh) / 2) + dh
    };
    // var partialCanvas = document.getElementById("partialCanvas");
    // partialCanvas.width = dw;
    // partialCanvas.height = dh;

    // var canvas = document.getElementById("streamCanvas");
    var context = canvas.getContext("2d");

    context.beginPath();

    context.moveTo(topleftCorner.x, topleftCorner.y);
    context.lineTo(topRightCorner.x, topRightCorner.y);
    context.strokeStyle = "#FFF000";
    context.lineWidth = rectWidth;
    context.stroke();


    context.beginPath();
    context.moveTo(topleftCorner.x, topleftCorner.y);
    context.lineTo(bottomLeftCorner.x, bottomLeftCorner.y);
    context.strokeStyle = "#FFF000";
    context.lineWidth = rectWidth;
    context.stroke();

    context.beginPath();
    context.moveTo(bottomLeftCorner.x, bottomLeftCorner.y);
    context.lineTo(bottomRightCorner.x, bottomRightCorner.y);
    context.strokeStyle = "#FFF000";
    context.lineWidth = rectWidth;
    context.stroke();

    context.beginPath();
    context.moveTo(topRightCorner.x, topRightCorner.y);
    context.lineTo(bottomRightCorner.x, bottomRightCorner.y);
    context.strokeStyle = "#FFF000";
    context.lineWidth = rectWidth;
    context.stroke();

    // var partialCanvasContext = document.getElementById("partialCanvas").getContext("2d");
    if (canvas.width > 0) {
        GetRectangleAndCheckResult(topleftCorner, dw, dh);

        context.save();
        context.textAlign = 'center';
        context.fillStyle = '#000';
        // context.translate(topleftCorner.x + (dw / 2) - 10, topleftCorner.x + (dh / 2) - 10);
        context.translate(topleftCorner.x + (dw / 2) - 5, topleftCorner.y + (dh / 2));
        context.rotate(Math.PI / 2);
        context.font = '1.5rem Noto Sans Display';
        context.fillText(canvasHelperText, 0, 0)
        context.restore()

        //   context.drawImage(
        //       canvas,
        //       topleftCorner.x + 2,
        //       topleftCorner.y + 2,
        //       dw - 4,
        //       dh - 4,
        //       0,
        //       0,
        //       dw,
        //       dh
        //       );

    }

}
function GetRectangleAndCheckResult(topleft, width, height) {
    let src = cv.imread('streamCanvas');
    let dst = new cv.Mat();
    // You can try more different parameters
    let rect = new cv.Rect(topleft.x + rectWidth, topleft.y + rectWidth, width - rectWidth * 2, height - rectWidth * 2);
    dst = src.roi(rect);

    src.delete();
    dst.delete();
}
function BlobCapture(callback) {
    player.pause();
    stopStreaming = true;
    var canvas = document.getElementById("streamCanvas");

    canvas.toBlob(function (blob) {
        callback(blob)
    });
}
//function Capture() {

//    return new Promise((resolve) => {
//        player.pause();
//        stopStreaming = true;
//        var canvas = document.getElementById("streamCanvas");
//        var ctx = canvas.getContext("2d")
//        let img = new Image()
//        img.src = canvas.toDataURL()
//        img.onload = () => {

//            let width = img.width
//            let height = img.height
//            if (width > height) {
//                canvas.width = width
//                canvas.height = height

//                ctx.drawImage(img, 0, 0, width, height)
//                resolve(resizeImage(canvas.toDataURL()))
//            }
//            else {
//                canvas.width = height;
//                canvas.height = width;
//                ctx.translate(0, width);
//                ctx.rotate(-90 * Math.PI / 180)
//                ctx.drawImage(img, 0, 0, width, height)
//                resolve(resizeImage(canvas.toDataURL()))
//            }

//        }

//    })
//}




function RequestPassportData(file) {
    /*var form = new FormData();*/
    /*form.append('File', file)*/
    //$('#File').val(file);

    document.querySelector('#CanvasForm #File').value = file;
    $('#CanvasForm').submit();
    //var settings = {
    //    "url": "/home/GetOcrResult",
    //    "method": "POST",
    //    "processData": false,
    //    "mimeType": "multipart/form-data",
    //    "contentType": false,
    //    "data": form
    //};

    //$.ajax(settings).done(function (response) {
    //    console.log(response);
    //});
}
function dataURLtoFile(dataurl, filename) {
    var arr = dataurl.split(','), mime = arr[0].match(/:(.*?);/)[1],
        bstr = atob(arr[1]), n = bstr.length, u8arr = new Uint8Array(n);
    while (n--) {
        u8arr[n] = bstr.charCodeAt(n);
    }
    return new File([u8arr], filename, { type: mime });
}
function spin() {
    document.querySelector('.spin').classList.remove('d-none');
}
function unspin() {
    document.querySelector('.spin').classList.add('d-none');
}

function CaptureAndProccess() {
    document.querySelector('.capture-btn').classList.add('disabled')

    BlobCapture(function (blob) {

        if (isSafari()) {
            exitFullscreenCanvas()
        }
        else {
            exitFullscreen();
        }
        spin()
        document.querySelector('.cameraDiv').classList.add('d-none');


        let file = new File([blob], "img.jpg");
        let container = new DataTransfer();
        container.items.add(file);
        document.querySelector('#resultForm #File').files = container.files;
        //$('#resultForm').submit();
      
        var reader = new FileReader();
        var imgtag = document.querySelector(".camera-preview-img");
        reader.onload = function (event) {
            imgtag.src = event.target.result;
            document.querySelector('.canvascontainer').classList.add('d-none');
            $('#camraPreview').removeClass('d-none');
            $('.cameraDiv').removeClass('d-none');
            $('.camera-preview-img').removeClass('d-none');
        };

        unspin();
        reader.readAsDataURL(file);
    });

    //Capture().then(function (img) {
    //    if (isSafari()) {
    //        exitFullscreenCanvas()
    //    }
    //    else {
    //        exitFullscreen();
    //    }
    //    spin()
    //    document.querySelector('.cameraDiv').classList.add('d-none');
    //    RequestPassportData(img);
    //});

}

function submitForm(file=false) {
    if (file) {
        $('#resultForm').submit();
    } else {
        $('#CanvasForm').submit();
    }


}

function resizeImage(base64Str, maxWidth = 800, maxHeight = 800) {
    return new Promise((resolve) => {
        let img = new Image()
        img.src = base64Str
        img.onload = () => {
            let canvas = document.createElement('canvas')
            const MAX_WIDTH = maxWidth
            const MAX_HEIGHT = maxHeight
            let width = img.width
            let height = img.height

            if (width > height) {
                if (width > MAX_WIDTH) {
                    height *= MAX_WIDTH / width
                    width = MAX_WIDTH
                }
            } else {
                if (height > MAX_HEIGHT) {
                    width *= MAX_HEIGHT / height
                    height = MAX_HEIGHT
                }
            }
            canvas.width = width
            canvas.height = height
            let ctx = canvas.getContext('2d')
            ctx.drawImage(img, 0, 0, width, height)
            resolve(canvas.toDataURL())
        }
    })
}
