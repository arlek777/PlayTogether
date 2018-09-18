import * as React from 'react';

class Profile extends React.Component<{}, {}> {
    constructor(props: any) {
        super(props);
    }

    public render() {
        return <div className="row">
                   <div className="col-md">
                       { this.renderBasicInfo() }
                   </div>
               </div>;
    }

    private renderPhotoEditor() {
        
    }

    private renderBasicInfo() {
        return <div>
                   <div className='form-group'>
                       <label htmlFor="name">Псевдоним/Имя</label>
                       <input type="text" className="form-control" id="name" placeholder="Псевдоним/Имя"/>
                   </div>
                   <div className='form-group'>
                       <label htmlFor="email">Контактный E-mail</label>
                       <input type="email" className="form-control" id="email" placeholder="Контактный E-mail"/>
                   </div>
                   <div className='form-group'>
                       <label htmlFor="phone1">Телефон 1</label>
                       <input type="text" className="form-control" id="phone1" placeholder="Телефон 1"/>
                   </div>
                   <div className='form-group'>
                       <label htmlFor="phone2">Телефон 2</label>
                       <input type="text" className="form-control" id="phone2" placeholder="Телефон 2"/>
                   </div>
                   <div className='form-group'>
                       <label htmlFor="city">Город</label>
                       <input type="text" className="form-control" id="city" placeholder="Город"/>
                   </div>
                   <div className='form-group'>
                       <label htmlFor="address">Адрес</label>
                       <input type="text" className="form-control" id="address" placeholder="Адрес"/>
                   </div>
                   <div className='form-group'>
                       <label htmlFor="experience">Опыт в годах</label>
                       <input type="number" className="form-control" id="experience" placeholder="Опыт в годах"/>
                   </div>
                   <div className='form-group'>
                       <label htmlFor="description">Подробное описание</label>
                       <input type="textarea" className="form-control" id="description" placeholder="Подробное описание"/>
                   </div>
               </div>;
    }
}