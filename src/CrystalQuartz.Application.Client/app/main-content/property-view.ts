﻿import { Property } from '../api';

export class PropertyValue implements js.IView<Property> {
    template = '<span></span>';

    init(dom: js.IDom, value: Property) {
        if (value == null || value.Value == null) {
            dom.$.addClass('none');
        } else {
            dom.$.append(value.Value);

            if (value.TypeName) {
                dom.$.addClass(value.TypeName.toLowerCase());
            }
        }
    }
}

export class PropertyView implements js.IView<Property> {
    template = '<tr>' +
    '<td class="name"></td>' +
    '<td class="value"></td>' +
    '</tr>';

    init(dom: js.IDom, value: Property) {
        dom('.name').observes(value.Name);
        dom('.value').observes(value, PropertyValue);
    }
}

export class PropertyWithTypeView implements js.IView<Property> {
    template = '<tr>' +
    '<td class="name"></td>' +
    '<td class="value"></td>' +
    '<td class="type"><span class="runtimetype"></span></td>' +
    '</tr>';

    init(dom: js.IDom, value: Property) {
        dom('.name').observes(value.Name);
        dom('.value').observes(value, PropertyValue);
        dom('.type span').observes(value.TypeName);
    }
}  