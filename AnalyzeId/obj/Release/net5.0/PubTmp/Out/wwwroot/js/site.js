HaDateTimePicker.init();
function spin1() {
    document.querySelector('.spin').classList.remove('d-none');
    document.querySelector('.spin').style.display = "block"
}
function unspin1() {
    document.querySelector('.spin').classList.add('d-none');
    document.querySelector('.spin').style.display = "none"
}

function SubmitSignature() {
    document.getElementById('submit-form-btn').classList.add('disabled')

    var isEmpty = signaturePad.isEmpty();
    if (isEmpty) {
        var validation = document.getElementsByClassName('validation')[0];
        validation.classList.remove('d-none');
        validation.innerHTML = `<p class='mb-0 p-1'>Please Sign the pad.</p>`;
    }
    else {
        var img = signaturePad.toDataURL().split(',')[1];
        var blob = b64toBlob(img);
        $("#procces").removeClass("d-none")
        $("#sigshow").addClass("d-none")
            var trId = $("#transactionId").val()
            var type = blob.type
            $.post("/Home/Signature", { file: img, type: type, transactionId: trId }, function (res) {
                if (res == "true") {
                    window.location.replace('/Home/ThankYou');
                } else {
                    var validation = document.getElementsByClassName('validation')[0];
                    validation.classList.remove('d-none');
                    validation.innerHTML = `<p class='mb-0 p-1'>${res}.</p>`;
                }
                $("#procces").addClass("d-none")
                $("#sigshow").removeClass("d-none")
            })
      

        //$("#signature-file").attr("value", new File([blob], "img.png"))
        //UploadFile(blob).then(function (response) {
        //    if (response.data.Message.Error == false) {
        //        console.log(response.data);
        //        ExecuteApplication(true).then(function (response) {
        //            console.log(response.data);
        //            window.location.replace('/Home/ThankYou');
        //        });
        //    }
        //    else {
        //        console.log(response.data);
        //        document.getElementById('submit-form-btn').classList.remove('disabled')
        //    }
        //});
        //window.location.replace('/Home/ThankYou');
    }
}

function UploadFileInDvice(file) {
    return new Promise((resolve) => {
        $("#signature-file").attr("value", new File([file], "img.png"))
        var senddata = $.get("/Home/Signature", { file: file }, function (res) {
            debugger;
        })
        resolve(senddata);
    })
}

function UploadFile(file) {
    return new Promise((resolve) => {

        // var data = file;
        var data = new FormData();
        data.append('File', new File([file], "img.png"));

        var config = {
            method: 'post',
            url: `${endpoint}/api/Forms/File`,
            headers: {
                'API-Username': apiUsername,
                'API-Password': apiPassword,
                'Application_ID': applicationId,
                'Element_Key': 'SIGN',
                'Element_Title': 'Signature',
                'ID_Document_Code': '0',
                'OCR': 'False',
                'Content-Type': 'image/png'
            },
            data: data
        };

        resolve(axios(config));
    })
}
const b64toBlob = (b64Data, contentType = 'image/png', sliceSize = 512) => {
    const byteCharacters = atob(b64Data);
    const byteArrays = [];

    for (let offset = 0; offset < byteCharacters.length; offset += sliceSize) {
        const slice = byteCharacters.slice(offset, offset + sliceSize);

        const byteNumbers = new Array(slice.length);
        for (let i = 0; i < slice.length; i++) {
            byteNumbers[i] = slice.charCodeAt(i);
        }

        const byteArray = new Uint8Array(byteNumbers);
        byteArrays.push(byteArray);
    }

    const blob = new Blob(byteArrays, { type: contentType });
    return blob;
}

let rotateImage = () => {
    spin1()
    document.querySelector('.cameraDiv').classList.add('d-none');
    let img = new Image();
    img.src = document.getElementsByClassName('camera-preview-img')[0].src;
    let canvas = document.createElement("canvas");
    img.onload = function () {
        rotateImage();
        saveImage(img.src.replace(/^.*[\\\/]/, ''));
    }
    let rotateImage = () => {
        let ctx = canvas.getContext("2d");
        canvas.width = img.height;
        canvas.height = img.width;
        ctx.translate(canvas.width / 2, canvas.height / 2);
        ctx.rotate(Math.PI / 2);
        ctx.drawImage(img, -img.width / 2, -img.height / 2);
    }

    let saveImage = (img_name) => {
        let file64 = canvas.toDataURL("image/png");
        var file = dataBase64URLtoFile(file64, "a.png")
        document.getElementById('File').value = "";
        let list = new DataTransfer();
        list.items.add(file);
        document.getElementById('File').files = list.files;
        $(".camera-preview-img").attr("src", file64)
        unspin1()
        document.querySelector('.cameraDiv').classList.remove('d-none');
        //a.download = img_name;
        //document.body.appendChild(a);
        //a.click();
    }
}

function dataBase64URLtoFile(dataurl, filename) {
    var arr = dataurl.split(','), mime = arr[0].match(/:(.*?);/)[1],
        bstr = atob(arr[1]), n = bstr.length, u8arr = new Uint8Array(n);
    while (n--) {
        u8arr[n] = bstr.charCodeAt(n);
    }
    return new File([u8arr], filename, { type: mime });
}