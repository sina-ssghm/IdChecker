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
        debugger;

        $("#procces").removeClass("d-none")
        $("#sigshow").addClass("d-none")
            var trId = $("#transactionId").val()
            var type = blob.type
            $.post("/Home/Signature", { file: img, type: type, transactionId: trId }, function (res) {
                debugger
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