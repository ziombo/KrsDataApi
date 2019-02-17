import {Component} from '@angular/core';
import {HttpClient, HttpEventType} from '@angular/common/http';
import {saveAs, saveFile} from 'file-saver';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  apiEndpoint = 'https://localhost:44345/api/krsdata';
  selectedFile: File = null;
  dropzoneActive: boolean = false;

  constructor(private httpClient: HttpClient) {
  }


  onFileSelected(event) {
    this.selectedFile = event.target.files[0] as File;
  }

  onUpload() {
    const fd = new FormData();
    fd.append('file', this.selectedFile, this.selectedFile.name);
    this.httpClient.post(this.apiEndpoint, fd, {responseType: 'blob', reportProgress: true, observe: 'events'})
      .subscribe(event => {

        if (event.type === HttpEventType.UploadProgress) {
          console.log('Upload progress: ' + Math.round(event.loaded / event.total * 100) + '%');
        }
        else if (event.type === HttpEventType.Response) {
          const blob = new Blob([event.body], {type: 'application/octet-stream'});
          saveAs(blob, 'Rejestr KRS.xlsx');
        }
      });
  }

  dropzoneState($event: boolean) {
    console.log($event);
    this.dropzoneActive = $event;
  }

  handleDrop(fileList: FileList) {
    this.selectedFile = fileList[0];

    // let filesIndex = _.range(fileList.length)
    //
    // _.each(filesIndex, (idx) => {
    //   this.currentUpload = new Upload(fileList[idx]);
    //   this.upSvc.pushUpload(this.currentUpload)}
    // )
  }
}
